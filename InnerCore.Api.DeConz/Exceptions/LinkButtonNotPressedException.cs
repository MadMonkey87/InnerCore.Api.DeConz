using System;

namespace InnerCore.Api.DeConz.Exceptions
{
    public class LinkButtonNotPressedException : Exception
    {
        public LinkButtonNotPressedException(string message)
            : base(message) { }
    }
}
