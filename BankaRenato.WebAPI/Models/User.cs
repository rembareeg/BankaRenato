using System;
using System.Collections.Generic;

namespace BankaRenato.WebAPI.Models
{
    public partial class User
    {
        public User()
        {
            Account = new HashSet<Account>();
        }

        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] Password { get; set; }
        public Guid Salt { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Account> Account { get; set; }
    }
}
