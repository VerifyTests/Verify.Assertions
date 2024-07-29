namespace VerifyTests;

public static class VerifyAssertions
{
    public static bool Initialized { get; private set; }

    public static void Initialize()
    {
        if (Initialized)
        {
            throw new("Already Initialized");
        }

        Initialized = true;

        InnerVerifier.ThrowIfVerifyHasBeenRun();
        VerifierSettings.AddExtraSettings(_ => _.Serialized += Serialized);
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
        var asserts = GetAsserts(context);
        var type = typeof(T);
        Action<object> wrapped = _ => assert((T) _);
        if (asserts.TryGetValue(type, out var assertsForType))
        {
            assertsForType.Add(wrapped);
        }
        else
        {
            asserts[type] = [wrapped];
        }
    }

    static Dictionary<Type, List<Action<object>>> GetAsserts(Dictionary<string, object> context)
    {
        if (TryGetAsserts(context, out var value))
        {
            return value;
        }

        Dictionary<Type, List<Action<object>>> asserts = [];
        context["AssertType"] = asserts;
        return asserts;
    }

    static bool TryGetAsserts(IReadOnlyDictionary<string, object> context, [NotNullWhen(true)] out Dictionary<Type, List<Action<object>>>? value)
    {
        if (context.TryGetValue("AssertType", out var list))
        {
            value = (Dictionary<Type, List<Action<object>>>) list;
            return true;
        }

        value = null;
        return false;
    }

    static void Serialized(JsonWriter writer, object target)
    {
        var verifyJsonWriter = (VerifyJsonWriter) writer;
        if (!TryGetAsserts(verifyJsonWriter.Context, out var asserts))
        {
            return;
        }

        var targetType = target.GetType();
        foreach (var (type, actions) in asserts)
        {
            if (!targetType.IsAssignableTo(type))
            {
                continue;
            }

            foreach (var action in actions)
            {
                action(target);
            }
        }
    }
}