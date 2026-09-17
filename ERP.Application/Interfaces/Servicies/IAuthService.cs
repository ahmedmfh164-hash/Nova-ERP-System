using ERP.Contacts.Requests.Auth;
using ERP.Contacts.Responses.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Application.Interfaces.Servicies
{
    public interface IAuthService
    {
        public Task<TokenResponseDTO> LoginAsync(LoginRequestDTO login);
        public Task<TokenResponseDTO> RefreshAsync(RefreshRequestDTO request);
        public Task<TokenResponseDTO> LogoutAsync(LogoutRequestDTO request);

    }
}
