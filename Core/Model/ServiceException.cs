using System;
using System.Collections;

namespace Core.Model
{
    public class ServiceException : Exception
    {
        public const string UnknownErrorCode = "UNKNOWN";

        public string ErrorCode { get; }

        public ICollection Errors { get; }
        

        public ServiceException(string errorCode, ICollection errors = null)
            : base("See message by errorCode = '" + errorCode + "'")
        {
            ErrorCode = errorCode;
            Errors = errors;
        }
        
    }
}