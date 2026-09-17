using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Core
{
    public class DeletedPersonResult
    {
        public int PersonId { get; set; }
        public Guid? ImageGuid { get; set; }

        public DeletedPersonResult(int personId, Guid? imageGuid)
        {
            PersonId=personId;
            ImageGuid=imageGuid;
        }
    }
}
