namespace MarsRover.Parser;

public record class Discardable(string Value, Func<Exception, ParseException> Discard)
{
    public void Try(Action<string> action)
    {
        try
        {
            action(Value);
        }
        catch (Exception exception)
        {
            throw Discard(exception);
        }
    }

    public Discardable(
        (string line, int index)[] currentTook,
        string Value, string purpose, Func<Exception, string> errorMessage, int index
    )
        : this(Value, GetDiscard(currentTook, purpose, errorMessage, index)) { }

    public static Func<Exception, ParseException> GetDiscard(
        (string line, int index)[] currentTook,
        string purpose, Func<Exception, string> errorMessage, int index)
    {
        return (exception) => {
            string debugLines = "";
            if (index == LinesParser.debugLines_Last)
            {
                var last = currentTook.Last();
                debugLines = $"LINE:{last.index}: {last.line}";
            }
            else if (index == LinesParser.debugLines_All)
            {
                debugLines = currentTook
                    .Select(line => $"LINE:{line.index}: {line.line}")
                    .Aggregate((x, y) => x + " -- " + y);
            }
            else
            {
                var line = currentTook[index];
                debugLines = $"LINE:{line.index}: {line.line}";
            }

            throw new ParseException(@$"parse error: {purpose} {errorMessage(exception)} -- {debugLines}.");
        };
    }
}