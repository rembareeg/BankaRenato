using System;
using System.Collections.Generic;

namespace BankaRenato.WebAPI.Models
{
    public partial class Role
    {
        public Role()
        {
            User = new HashSet<User>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
