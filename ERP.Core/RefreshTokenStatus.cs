using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Core
{
    public static class RefreshTokenStatus
    {
        public static string? RefreshTokenHash { get; set; }
        public static DateTime? RefreshTokenRevokedAt { get; set; }
        public static DateTime? RefreshTokenExpiresAt { get; set; }

    }
}
