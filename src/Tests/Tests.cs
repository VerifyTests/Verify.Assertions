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

    #region Shared

    [ModuleInitializer]
    public static void AddSharedAssert() =>
        VerifyAssertions
            .Assert<SharedNested>(
                _ => Assert.Equal("value", _.Property));

    [Fact]
    public async Task SharedAssert()
    {
        var nested = new SharedNested(Property: "value");
        var target = new SharedTarget(nested);
        await Verify(target);
    }

    #endregion

    static bool sharedCalled;

    [ModuleInitializer]
    public static void AddSharedAssertForTest() =>
        VerifyAssertions
            .Assert<SharedNested>(
                _ =>  sharedCalled = true);

    [Fact]
    public async Task SharedAssertCalled()
    {
        var nested = new SharedNested(Property: "value");
        var target = new SharedTarget(nested);
        await Verify(target);
        Assert.True(sharedCalled);
    }

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