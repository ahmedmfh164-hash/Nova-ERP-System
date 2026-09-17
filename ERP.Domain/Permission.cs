using ERP.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Domain
{
    public sealed record Permission (PermissionModules Module,PermissionAction Action)
    {
        public override string ToString()
            => $"{Module}.{Action}";
        
    }
}
