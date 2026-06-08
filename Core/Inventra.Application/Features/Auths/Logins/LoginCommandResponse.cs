using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Auths.Logins
{
    public class LoginCommandResponse
    {
        public string AccessToken { get; set; }

        public DateTime ExpirationTime { get; set; }
    }
}
