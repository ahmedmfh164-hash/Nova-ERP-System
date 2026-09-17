using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Core
{
    public class StoredFile
    {
        public Stream Stream { get; set; } = null!;

        public string FileName { get; set; } = null!;

        public string ContentType { get; set; } = null!;
    }
}
