namespace Injector_UI
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
