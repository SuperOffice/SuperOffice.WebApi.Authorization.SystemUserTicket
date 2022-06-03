using System;
using System.Net.Http.Headers;
using System.Runtime.Serialization;

namespace SuperOffice.WebApi.Authorization.SystemUserTicket.Exceptions
{
    /// <summary>
    /// Thrown when exception occurs obtaining a new SystemUser ticket.
    /// </summary>
    [Serializable]
    public class SystemUserException : Exception
    {
        /// <summary>
        /// Default - all blank
        /// </summary>
        public SystemUserException() : base($"Error occured in {nameof(AuthorizationSystemUserTicket)}")
        {
        }

        /// <summary>
        /// Message
        /// </summary>
        /// <param name="message">"halp!"</param>
        public SystemUserException(string message) : base(message)
        {
        }

        /// <summary>
        /// Wrapper
        /// </summary>
        /// <param name="message">"Halp!"</param>
        /// <param name="innerException">the real problem</param>
        public SystemUserException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
