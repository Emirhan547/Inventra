using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Options
{
    public class JwtSettings
    {
        public string Issuer { get; set; }

        public string Audience { get; set; }

        public string SecretKey { get; set; }

        public int ExpireMinutes { get; set; }
    }
}
