using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using $safeprojectname$.Infrastructure;
using $safeprojectname$.ViewModels;
using $ext_safeprojectname$.DAL.Entities;
using $ext_safeprojectname$.Infrastructure.Enums;
using $ext_safeprojectname$.Infrastructure.Helpers;
using $ext_safeprojectname$.ServiceLayer.Services;

namespace $safeprojectname$.Controllers
{
    [Route("api/[controller]")]
    public class TokenController : BaseController
    {
        private readonly IUserService _userService;
        private readonly TokenProviderOptions _options;
        public TokenController(IUserService userService,
            IOptions<TokenProviderOptions> options,
            IOptions<AppSettings> settings)
        {
            _userService = userService;
            _options = options.Value;

            var appSettings = settings.Value;
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.SecretKey));
            _options.SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            _options.Audience = "$projectname$Aud";
            _options.Issuer = "$projectname$";
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Post([FromForm] TokenViewModel model)
        {
            var serviceResult = new ServiceResult();
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception(GetModelStateErrors());
                var result = await _userService.GetEntityAsync<User>(c => c.Email == model.UserName);
                if (result == null)
                {
                    throw new Exception("User name or password is incorrect");
                }

                var isVerified = _userService.VerifyHashedPassword(result.PasswordHash, model.Password);

                if (!isVerified)
                    throw new Exception("User name or password is incorrect");

                var now = DateTime.UtcNow;

                // Specifically add the jti (random nonce), iat (issued timestamp), and sub (subject/user) claims.
                // You can add other claims here, if you want:
                var claims = new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Sub, model.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, now.Second.ToString(), ClaimValueTypes.Integer64),
                    new Claim(JwtRegisteredClaimNames.Iat, now.Second.ToString(), ClaimValueTypes.Integer64),
                    new Claim("userId", result.Id.ToString()),
                    new Claim("roles", result.Role)
                };

                var jwt = new JwtSecurityToken(
                    issuer: _options.Issuer,
                    audience: _options.Audience,
                    claims: claims,
                    notBefore: now,
                    expires: now.Add(_options.Expiration),
                    signingCredentials: _options.SigningCredentials);

                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

                var response = new
                {
                    access_token = encodedJwt,
                    expires_on = now.Add(_options.Expiration)
                };

                serviceResult.Success = true;
                serviceResult.Messages.AddMessage(MessageType.Info, "Successfully logged in.");
                serviceResult.Data = response;
            }
            catch (Exception ex)
            {
                CreateErrorResult(serviceResult, ex);
            }

            return Json(serviceResult);
        }
    }
}