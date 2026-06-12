using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Application.Abstractions.Messaging
{
    public interface IEventBus
    {
        Task PublishAsync<T>(T message) where T : class;
    }
}
