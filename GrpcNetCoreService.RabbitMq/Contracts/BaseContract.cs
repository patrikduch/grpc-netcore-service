using System;

namespace GrpcNetCoreService.RabbitMq.Contracts
{
    public interface BaseContract
    {
        Guid EventId { get; }
        DateTime Timestamp { get; }
    }
}
