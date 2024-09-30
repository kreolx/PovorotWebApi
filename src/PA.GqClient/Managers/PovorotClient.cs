using PA.Contracts.Interfaces;
using StrawberryShake;

namespace PA.GqClient.Managers;

internal sealed class PovorotClient(ConferenceClient client) : IPovorotClient
{
    public async Task<IEnumerable<DateTimeOffset>> GetEmptySlots(DateTimeOffset dateStart, DateTimeOffset dateEnd, CancellationToken cancellationToken)
    {
        var result = await client.GetBusyRequests.ExecuteAsync(dateStart, dateEnd, cancellationToken);
        result.EnsureNoErrors();
        var allTimes = Enumerable.Range(9, 10)
            .Select(x => new DateTimeOffset(dateStart.Year, dateStart.Month, dateStart.Day, x, 0, 0, TimeSpan.FromHours(10)))
            .ToArray();
        var busySlots = result.Data!.Requests.Select(x => x.RequestedAt);
        busySlots = busySlots.GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.ToArray().Length)
            .Where(x => x.Value == 3)
            .Select(x => x.Key);
        var slots = allTimes.Except(busySlots).ToArray();
        return slots;
    }
}