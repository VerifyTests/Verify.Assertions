namespace VerifyTests;

public static class VerifyAssertions
{
    static List<Action<object>> sharedAsserts = [];

    public static void Assert<T>(Action<T> assert) =>
        sharedAsserts.Add(Wrap(assert));

    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings.AddExtraSettings(_ => _.Serializing += Serializing);
    }

    [Pure]
    public static SettingsTask Assert<T>(this SettingsTask settings, Action<T> assert)
    {
        settings.CurrentSettings.Assert(assert);
        return settings;
    }

    public static void Assert<T>(this VerifySettings settings, Action<T> assert)
    {
        var context = settings.Context;
        if (TryGetAsserts(context, out var asserts))
        {
            asserts.Add(Wrap(assert));
            return;
        }

        context["AssertList"] =
            new List<Action<object>>
            {
                Wrap(assert)
            };
    }

    static Action<object> Wrap<T>(Action<T> assert) =>
        _ =>
        {
            if (_ is T t)
            {
                assert(t);
            }
        };

    static bool TryGetAsserts(
        IReadOnlyDictionary<string, object> context,
        [NotNullWhen(true)] out List<Action<object>>? value)
    {
        if (context.TryGetValue("AssertList", out var list))
        {
            value = (List<Action<object>>) list;
            return true;
        }

        value = null;
        return false;
    }

    static void Serializing(JsonWriter writer, object target)
    {
        var verifyWriter = (VerifyJsonWriter) writer;

        HandleAsserts(sharedAsserts, target);

        if (TryGetAsserts(verifyWriter.Context, out var asserts))
        {
            HandleAsserts(asserts, target);
        }
    }

    static void HandleAsserts(List<Action<object>> actions, object target)
    {
        foreach (var action in actions)
        {
            action(target);
        }
    }
}