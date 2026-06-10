using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Users.Results
{
    public class GetUserByIdQueryResponse
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
