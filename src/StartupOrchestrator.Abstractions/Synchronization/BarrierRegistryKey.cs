namespace StartupOrchestrator.Abstractions.Synchronization
{
    public record BarrierRegistryKey
    {
        public required Type Type { get; set; }
        public string? Key { get; set; }
    }
}
