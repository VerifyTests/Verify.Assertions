using FluentAssertions;

public class FluentAssertionsTests
{
    #region FluentAssertionsUsage

    [Fact]
    public async Task FluentAssertionsUsage()
    {
        var nested = new Nested(Property: "value");
        var target = new Target(nested);
        await Verify(target)
            .Assert<Nested>(
                _ => _.Property.Should().Be("value"));
    }

    #endregion
}
