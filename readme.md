# <img src="/src/icon.png" height="30px"> Verify.Assertions

[![Discussions](https://img.shields.io/badge/Verify-Discussions-yellow?svg=true&label=)](https://github.com/orgs/VerifyTests/discussions)
[![Build status](https://ci.appveyor.com/api/projects/status/89flq4nfrcmnykd0?svg=true)](https://ci.appveyor.com/project/SimonCropp/Verify-Assertions)
[![NuGet Status](https://img.shields.io/nuget/v/Verify.Assertions.svg)](https://www.nuget.org/packages/Verify.Assertions/)

Extends [Verify](https://github.com/VerifyTests/Verify) to allow an assertion callback. This enables using assertion libaries to interrogate during serialization.

**See [Milestones](../../milestones?state=closed) for release notes.**

## NuGet package

https://nuget.org/packages/Verify.Assertions/


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


### Xunit

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


### NUnit

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


### FluentAssertions

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


### Shouldly

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


## Icon

[Approval](https://thenounproject.com/term/correct/6480102/) designed by [Danang Marhendra](https://thenounproject.com/creator/masart/) from [The Noun Project](https://thenounproject.com/).
