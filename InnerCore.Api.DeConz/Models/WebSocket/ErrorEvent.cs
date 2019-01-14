using System;

namespace InnerCore.Api.DeConz.Models.WebSocket
{
    public class ErrorEvent : EventArgs
    {
        public ErrorEvent(Exception ex)
        {
            Ex = ex;
        }

        public Exception Ex { get; private set; }
    }
}
