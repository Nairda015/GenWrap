using System.Diagnostics.CodeAnalysis;

namespace GenWrap.Examples.ChartExample;

[ExcludeFromCodeCoverage]
public class Chart
{
    public List<ChartPoint> SimplifyPriceChangedSet(IEnumerable<PriceChangedEvent> events)
    {
        var result = events
            .GroupBy(x => x.DateTime.Year)
            .Select(x => new ChartPoint
            {
                X = x.Key,
                Y = Math.Round(x.Average(e => e.Price), 2)
            })
            .ToList();

        return result;
    }
}

public record PriceChangedEvent(double Price, DateTime DateTime);

public record ChartPoint
{
    public required int X { get; init; }
    public required double Y { get; init; }
}