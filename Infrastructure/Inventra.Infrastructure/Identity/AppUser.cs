using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string? RefreshToken { get; set; }

        public DateTime? RefreshTokenExpireDate { get; set; }
    }
}
