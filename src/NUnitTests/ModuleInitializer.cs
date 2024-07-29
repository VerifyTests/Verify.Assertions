public static class ModuleInitializer
{
    [ModuleInitializer]
    public static void InitOther() =>
        VerifierSettings.InitializePlugins();
}