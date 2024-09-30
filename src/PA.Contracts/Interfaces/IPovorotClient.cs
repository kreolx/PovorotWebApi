namespace PA.Contracts.Interfaces;

public interface IPovorotClient
{
    Task<IEnumerable<DateTimeOffset>> GetEmptySlots(DateTimeOffset dateStart, DateTimeOffset dateEnd, CancellationToken cancellationToken);
}