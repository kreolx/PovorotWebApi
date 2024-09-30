using MassTransit;
using PA.Contracts.Interfaces;
using PA.Contracts.Models.Reporters;
using PL.Events;

namespace PA.BW.Reporter;

internal sealed class PovorotReporter(IPublishEndpoint publishEndpoint) : IPovorotEngineReporter
{
    public async Task NotifyAsync(AddRequestDto requestDto, CancellationToken cancellationToken)
    {
        var message = new Povorot.AddRequest.Requested(
            Guid.NewGuid(),
            requestDto.CarModel,
            requestDto.Phone,
            requestDto.Description, requestDto.RequestedAt
        );
        await publishEndpoint.Publish(message, cancellationToken);
    }
}