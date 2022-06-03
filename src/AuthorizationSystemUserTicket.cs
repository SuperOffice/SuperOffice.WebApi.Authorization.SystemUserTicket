
using System;
using System.Net.Http.Headers;
using SuperOffice.SystemUser;
using SuperOffice.WebApi.Authorization.SystemUserTicket.Exceptions;

namespace SuperOffice.WebApi.Authorization
{
    /// <summary>
    /// SOTicket for use with SystemUser tokens in CRM online only. See <seealso cref="IAuthorization"/>.
    /// </summary>
    public class AuthorizationSystemUserTicket : AuthorizationTicket, IAddHeaders
    {
        /// <summary>
        /// Ticket Authorization constructor used in CRM online scenario's where client secret is required.
        /// </summary>
        /// <param name="systemUserInfo">Contains information require to get System User ticket.</param>
        /// <param name="ticket">System user SOTicket string: "7T:abc123=="</param>
        public AuthorizationSystemUserTicket(SystemUserInfo systemUserInfo, string ticket) : base(ticket)
        {
            if (systemUserInfo == null)
                throw new ArgumentNullException(nameof(systemUserInfo), $"'{nameof(systemUserInfo)}' cannot be null or empty.");

            SystemUserInfo = systemUserInfo;
            RefreshAuthorization = RefreshAuthorizationMethod;
        }

        /// <summary>
        /// Contains information require to get System User ticket.
        /// </summary>
        public SystemUserInfo SystemUserInfo { get; }

        /// <summary>
        /// Called by the AgentBase and adds the SO-AppToken.
        /// </summary>
        /// <param name="headers">Collection of HttpRequest headers.</param>
        public void AddHeaders(HttpRequestHeaders headers)
        {
            headers.Add(Constants.Header.SOAppToken, SystemUserInfo.ClientSecret);
        }

        /// <summary>
        /// Try to refresh the existing ticket by exchanging the systemuser token for a system user ticket.
        /// </summary>
        /// <param name="e">Contains request, httpclient and exception leading here.</param>
        /// <returns>This IAuthorization with refreshed ticket.</returns>
        private IAuthorization RefreshAuthorizationMethod(ReAuthorizationArgs e)
        {
            // get the system user jwt

            var systemUserClient = new SystemUserClient(SystemUserInfo, e.Client);

            try
            {
                Ticket = systemUserClient.GetSystemUserTicketAsync().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                throw new SystemUserException($"Failed to get a new System User ticket in {nameof(AuthorizationSystemUserTicket)}.", ex);
            }

            return this;
        }
    }
}
