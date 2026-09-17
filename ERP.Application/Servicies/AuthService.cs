using ERP.Application.Interfaces.Repositories;
using ERP.Application.Interfaces.Servicies;
using ERP.Contacts.Requests.Auth;
using ERP.Contacts.Responses.Auth;
using ERP.Core;
using ERP.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ERP.Application.Servicies
{
    public class AuthService:IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository userRepository,IConfiguration configuration)
        {
            _userRepository=userRepository;
            _configuration=configuration;
        }


        public async Task<TokenResponseDTO> LoginAsync(LoginRequestDTO login)
        {
            if (string.IsNullOrWhiteSpace(login.Email)
               || string.IsNullOrWhiteSpace(login.Password))
            {
                return new TokenResponseDTO
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Status = TokenResponseDTO.ResultStatus.InvalidCredentials,
                    Message = "Invalid Credentials."
                };
            }

            var user = await _userRepository.GetUserByEmailAsync(login.Email);

            if (!PasswordHasher.Verify(login.Password, user.Password))
                return new TokenResponseDTO
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Status = TokenResponseDTO.ResultStatus.InvalidCredentials,
                    Message = "Invalid Credentials."
                };

            return await GenerateToken(user);
        }


        public async Task<TokenResponseDTO> RefreshAsync(RefreshRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Email)
               || string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return new TokenResponseDTO
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Status = TokenResponseDTO.ResultStatus.InvalidCredentials,
                    Message = "Invalid Credentials."
                };
            }

            if (RefreshTokenStatus.RefreshTokenRevokedAt != null)
                return new TokenResponseDTO
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Status = TokenResponseDTO.ResultStatus.Revoked,
                    Message = "Refresh token is revoked."
                };


            if (RefreshTokenStatus.RefreshTokenExpiresAt == null || RefreshTokenStatus.RefreshTokenExpiresAt <= DateTime.UtcNow)
                return new TokenResponseDTO
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Status = TokenResponseDTO.ResultStatus.Expired,
                    Message = "Refresh token expired."
                };


            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user==null)
                return new TokenResponseDTO
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Status = TokenResponseDTO.ResultStatus.InvalidCredentials,
                    Message = "Invalid Credentials."
                };

            bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, RefreshTokenStatus.RefreshTokenHash);
            if (!refreshValid)
                return new TokenResponseDTO
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Status = TokenResponseDTO.ResultStatus.Invalid,
                    Message = "Invalid refresh token."
                };

            return await GenerateToken(user);
        }


        public async Task<TokenResponseDTO> LogoutAsync(LogoutRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.Email)
                || string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                return new TokenResponseDTO
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Status = TokenResponseDTO.ResultStatus.InvalidCredentials,
                    Message = "Invalid Credentials."
                };
            }

            var user = await _userRepository.GetUserByEmailAsync(request.Email);

            if (user==null)
                return new TokenResponseDTO
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Status = TokenResponseDTO.ResultStatus.InvalidCredentials,
                    Message = "Invalid Credentials."
                };

            bool refreshValid = BCrypt.Net.BCrypt.Verify(request.RefreshToken, RefreshTokenStatus.RefreshTokenHash);
            if (!refreshValid)
                return new TokenResponseDTO
                {
                    AccessToken = null,
                    RefreshToken = null,
                    Status = TokenResponseDTO.ResultStatus.Invalid,
                    Message = "Invalid refresh token."
                };

            RefreshTokenStatus.RefreshTokenRevokedAt= DateTime.Now;

            return new TokenResponseDTO
            {
                AccessToken = null,
                RefreshToken = null,
                Status = TokenResponseDTO.ResultStatus.Revoked,
                Message = "Refresh token is revoked."
            };
        }

        private async Task<TokenResponseDTO> GenerateToken(User user)
        {
            var accessToken = await GenerateAccessToken(user);

            var refreshToken = GenerateRefreshToken();

            RefreshTokenStatus.RefreshTokenHash = BCrypt.Net.BCrypt.HashPassword(refreshToken);
            RefreshTokenStatus.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(7);
            RefreshTokenStatus.RefreshTokenRevokedAt = null;

            return new TokenResponseDTO
            {
                AccessToken = accessToken,
                RefreshToken=refreshToken,
                Status = TokenResponseDTO.ResultStatus.Success,
                Message = "Login Success."
            };

        }

        private async Task<string?> GenerateAccessToken(User user)
        {

            if (user== null)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.RoleName)
            };

            var key = new SymmetricSecurityKey(Encoding
                .UTF8.GetBytes(_configuration["ERP_JWT_SECRET_KEY"]!));

            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwt = _configuration.GetSection("Jwt");

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: signingCredentials);

            var accessToken = new JwtSecurityTokenHandler().WriteToken(token);
            return accessToken;
        }

        private string GenerateRefreshToken()
        {
            var bytes = new byte[64];

            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(bytes);

            return Convert.ToBase64String(bytes);
        }

       


    }
}
