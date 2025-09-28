namespace ScriptHook_GTA5_Injector
{
    public enum InjectionResult
    {
        Success,
        ProcessNotFound,
        AccessDenied,
        ArchitectureMismatch,
        DllNotFound,
        DllCorrupted,
        InjectionFailed,
        InitializationTimeout
    }
}
