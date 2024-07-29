[TestFixture]
public class Tests
{
    #region NUnitUsage

    [Test]
    public async Task NUnitUsage()
    {
        var nested = new Nested(Property: "value");
        var target = new Target(nested);
        await Verify(target)
            .Assert<Nested>(
                _ => Assert.That(_.Property, Is.EqualTo("value")));
    }

    #endregion
}