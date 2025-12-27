# <img src="/src/icon.png" height="30px"> Verify.Assertions

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://img.shields.io/appveyor/build/SimonCropp/Verify-Assertions)](https://ci.appveyor.com/project/SimonCropp/Verify-Assertions)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Assertions.svg)](https://www.nuget.org/packages/Verify.Assertions/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow assertion callbacks. This enables using assertion libraries to interrogate during serialization. The primary use case for this is when the data structures being verified are either complex or large.<!-- singleLineInclude: intro. path: /docs/intro.include.md -->

**See [Milestones](../../milestones?state=closed) for release notes.**


## Sponsors


### Entity Framework Extensions<!-- include: zzz. path: /docs/zzz.include.md -->

[Entity Framework Extensions](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.Assertions) is a major sponsor and is proud to contribute to the development this project.

[![Entity Framework Extensions](https://raw.githubusercontent.com/VerifyTests/Verify.Assertions/refs/heads/main/docs/zzz.png)](https://entityframework-extensions.net/?utm_source=simoncropp&utm_medium=Verify.Assertions)<!-- endInclude -->


## NuGet

 * https://nuget.org/packages/Verify.Assertions


## Enable

<!-- snippet: enable -->
<a id='snippet-enable'></a>
```cs
[ModuleInitializer]
public static void Init() =>
    VerifyAssertions.Initialize();
```
<sup><a href='/src/Tests/ModuleInitializer.cs#L3-L9' title='Snippet source file'>snippet source</a> | <a href='#snippet-enable' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Usage

Once enable, any assertion library can be used.

The below examples are simplistic for illustrating the usage. In a real world scenario, if data structures being verified are small, then the assertion can happen before or after the the Verify with no need to assert during serialization.


### [Xunit](https://xunit.net/)

<!-- snippet: XunitUsage -->
<a id='snippet-XunitUsage'></a>
```cs
[Fact]
public async Task XunitUsage()
{
    var nested = new Nested(Property: "value");
    var target = new Target(nested);
    await Verify(target)
        .Assert<Nested>(
            _ => Assert.Equal("value", _.Property));
}
```
<sup><a href='/src/Tests/Tests.cs#L3-L15' title='Snippet source file'>snippet source</a> | <a href='#snippet-XunitUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### [NUnit](https://docs.nunit.org/articles/nunit/writing-tests/assertions/assertions.html)

<!-- snippet: NUnitUsage -->
<a id='snippet-NUnitUsage'></a>
```cs
[Test]
public async Task NUnitUsage()
{
    var nested = new Nested(Property: "value");
    var target = new Target(nested);
    await Verify(target)
        .Assert<Nested>(
            _ => Assert.That(_.Property, Is.EqualTo("value")));
}
```
<sup><a href='/src/NUnitTests/Tests.cs#L4-L16' title='Snippet source file'>snippet source</a> | <a href='#snippet-NUnitUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### [FluentAssertions](https://fluentassertions.com/)

<!-- snippet: FluentAssertionsUsage -->
<a id='snippet-FluentAssertionsUsage'></a>
```cs
[Fact]
public async Task FluentAssertionsUsage()
{
    var nested = new Nested(Property: "value");
    var target = new Target(nested);
    await Verify(target)
        .Assert<Nested>(
            _ => _.Property.Should().Be("value"));
}
```
<sup><a href='/src/Tests/FluentAssertionsTests.cs#L5-L17' title='Snippet source file'>snippet source</a> | <a href='#snippet-FluentAssertionsUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


### [Shouldly](https://github.com/shouldly/shouldly)

<!-- snippet: ShouldlyUsage -->
<a id='snippet-ShouldlyUsage'></a>
```cs
[Fact]
public async Task ShouldlyUsage()
{
    var nested = new Nested(Property: "value");
    var target = new Target(nested);
    await Verify(target)
        .Assert<Nested>(
            _ => _.Property.ShouldBe("value"));
}
```
<sup><a href='/src/Tests/ShouldyAssertionsTests.cs#L5-L17' title='Snippet source file'>snippet source</a> | <a href='#snippet-ShouldlyUsage' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Shared Assertions

Assertions can be added globally.

<!-- snippet: Shared -->
<a id='snippet-Shared'></a>
```cs
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
```
<sup><a href='/src/Tests/Tests.cs#L17-L33' title='Snippet source file'>snippet source</a> | <a href='#snippet-Shared' title='Start of snippet'>anchor</a></sup>
<!-- endSnippet -->


## Icon

[Approval](https://thenounproject.com/term/correct/6480102/) designed by [Danang Marhendra](https://thenounproject.com/creator/masart/) from [The Noun Project](https://thenounproject.com/).
