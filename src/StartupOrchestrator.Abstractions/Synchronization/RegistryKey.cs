namespace StartupOrchestrator.Abstractions.Synchronization
{
    public record RegistryKey
    {
        public required Type Type { get; set; }
        public string? Key { get; set; }
    }
}
