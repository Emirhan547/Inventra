using Inventra.Application.Abstractions.Messaging;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;

namespace Inventra.Infrastructure.Messaging
{
    public sealed class MassTransitEventBus : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public MassTransitEventBus(
            IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task PublishAsync<T>(T message)
            where T : class
        {
            await _publishEndpoint.Publish(message);
        }
    }
}