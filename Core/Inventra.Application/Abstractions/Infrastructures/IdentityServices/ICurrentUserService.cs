using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Infrastructures.IdentityServices
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string UserName { get; }
    }
}
