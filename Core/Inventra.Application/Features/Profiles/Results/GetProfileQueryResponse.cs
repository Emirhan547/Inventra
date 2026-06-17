using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Features.Profiles.Results
{
    public sealed class GetProfileQueryResponse
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string FullName => $"{FirstName} {LastName}";

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public List<string> Roles { get; set; } = [];
    }
}
