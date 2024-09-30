using PA.Contracts.Models.Reporters;

namespace PA.Contracts.Interfaces;

public interface IPovorotEngineReporter
{
    Task NotifyAsync(AddRequestDto requestDto, CancellationToken cancellationToken);
}