using System;
using System.Collections.Generic;
using System.Text;

namespace ERP.Domain.Entities
{
    public class User
    {
        public int UserId { get; private set; }

        private string _UserName = null!;

        public string UserName
        {
            get => _UserName;
            set
            {
                ArgumentException.ThrowIfNullOrWhiteSpace("User name cannot be null or empty.");

                _UserName = value.Trim();
            }
        }

        public int PersonId { get; set; }
        public string? Password { get; set; }
        public string? RoleName { get; set; }
        public bool IsActive { get; set; }

        public User(int userId, string userName,int personId,string? password,string? roleName,bool isActive)
        {

            UserId=userId;
            UserName=userName;
            PersonId=personId;
            Password=password;
            RoleName=roleName;
            IsActive = isActive;
        }



    }
}
