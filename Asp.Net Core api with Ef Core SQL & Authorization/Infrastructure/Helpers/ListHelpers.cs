using System.Collections.Generic;
using $safeprojectname$.Enums;

namespace $safeprojectname$.Helpers
{
    public static class ListHelpers
    {
        public static void AddMessage(this List<Message<MessageType, string>> obj, MessageType type, string value)
        {
            obj.Add(new Message<MessageType, string>(type, value));
        }
    }
}