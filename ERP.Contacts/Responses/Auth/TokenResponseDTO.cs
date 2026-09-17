using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Contacts.Responses.Auth
{
    public class TokenResponseDTO
    {
        public enum ResultStatus
        {
            Success=1,
            InvalidCredentials,
            InactiveAccount,
            Revoked,
            Expired,
            Invalid,
            AlreadyLoggedIn
        }

        public string? AccessToken { get; init; } = null;
        public string? RefreshToken { get; init; } = null;
        public ResultStatus Status { get; init; } = ResultStatus.InvalidCredentials;
        public string? Message { get; init; } = null;
    }
}
