using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using Utility.Services;

namespace Data.Domain
{
    public class User : IdentityUser<Guid>, IDomain
    {
        public string Name { get; set; }
        public string PasswordSalt { get; set; }
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DeletedAt { get; set; }

        #region Navigation Props
        public virtual ICollection<Task> Tasks { get; set; }
        #endregion

        public void SetPassword(string password)
        {
            using (EncryptService service = new EncryptService())
            {
                (this.PasswordHash, this.PasswordSalt) = service.Encrypt(password);
            }
        }

        public bool IsPasswordEqualsTo(string password)
        {
            using (EncryptService service = new EncryptService()) return service.Encrypt(password, this.PasswordSalt).Equals(this.PasswordHash);
        }
    }
}
