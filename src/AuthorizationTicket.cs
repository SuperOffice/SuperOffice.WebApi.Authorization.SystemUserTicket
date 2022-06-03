using System;
using System.Net.Http.Headers;

namespace SuperOffice.WebApi.Authorization
{
    /// <summary>
    /// SOTicket for use in onsite scenarios. This does not support RefreshAuthorization. See also <seealso cref="IAuthorization"/>.
    /// </summary>
    public class AuthorizationTicket : IAuthorization
    {
        private string _ticket;

        /// <summary>
        /// Ticket Authorization constructor used in CRM onsite scenario's.
        /// </summary>
        /// <param name="ticket">SOTicket string: "7T:abc123=="</param>
        public AuthorizationTicket(string ticket)
        {
            if (string.IsNullOrEmpty(ticket))
                throw new ArgumentNullException(nameof(ticket), $"'{nameof(ticket)}' cannot be null or empty");
            _ticket = ticket;
        }

        /// <summary>
        /// Try to refresh this <seealso cref="IAuthorization"/> instance.
        /// </summary>
        public Func<ReAuthorizationArgs, IAuthorization> RefreshAuthorization { get; set; }

        /// <summary>
        /// The SOTicket value.
        /// </summary>
        protected string Ticket { get { return _ticket; } set { _ticket = value; } }

        /// <summary>
        /// Gets a tuple that represents authorization scheme and parameter for an SOTicket.
        /// </summary>
        /// <returns>Gets a <see cref="AuthenticationHeaderValue"/> (string,string) where scheme is SOTicket and parameter is SOTicket string.</returns>
        public AuthenticationHeaderValue GetAuthorization()
        {
            return new AuthenticationHeaderValue(Constants.Header.SOTicket, Ticket);
        }
    }
}
