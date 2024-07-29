using Shouldly;

public class ShouldlyTests
{
    #region ShouldlyUsage

    [Fact]
    public async Task ShouldlyUsage()
    {
        var nested = new Nested(Property: "value");
        var target = new Target(nested);
        await Verify(target)
            .Assert<Nested>(
                _ => _.Property.ShouldBe("value"));
    }

    #endregion
}
