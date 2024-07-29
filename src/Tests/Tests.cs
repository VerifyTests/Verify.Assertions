public class Tests
{
    #region XunitUsage

    [Fact]
    public async Task XunitUsage()
    {
        var nested = new Nested(Property: "value");
        var target = new Target(nested);
        await Verify(target)
            .Assert<Nested>(
                _ => Assert.Equal("value", _.Property));
    }

    #endregion

    [Fact]
    public async Task Inherited()
    {
        var nested = new Inherited(Property: "value");
        var target = new Target(nested);
        var called = false;
        await Verify(target)
            .Assert<Nested>(_ => called = true);
        Assert.True(called);
    }

    [Fact]
    public async Task EnsureAssertCalled()
    {
        var nested = new Nested(Property: "value");
        var target = new Target(nested);
        var called = false;
        await Verify(target)
            .Assert<Nested>(_ => called = true);
        Assert.True(called);
    }
}