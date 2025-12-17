using StartupOrchestrator.Abstractions.Extensions;
using StartupOrchestrator.Abstractions.Synchronization;

namespace StartupOrchestrator.Synchronization
{
    public sealed class BarrierRegistryContext : IBarrierRegistryContext
    {
        private readonly IReadOnlyDictionary<BarrierRegistryKey, object> _instances;

        public BarrierRegistryContext(IReadOnlyDictionary<BarrierRegistryKey, object> instances)
        {
            _instances = instances;
        }

        public T Get<T>()
            where T : notnull
        {
            var type = typeof(T);

            foreach (var (key, value) in _instances)
            {
                if (key.Type == type)
                    return (T)value;
            }

            throw new KeyNotFoundException($"No instance registered for type '{type.FullName}'.");
        }

        public T Get<T>(string? key)
            where T : notnull
        {
            if (_instances.TryGetValue(typeof(T).CreateKey(key), out var value))
                return (T)value;

            throw new KeyNotFoundException($"No instance registered for type '{typeof(T).FullName}' with key '{key ?? "<null>"}'.");
        }

        public bool TryGet<T>(out T? value) where T : notnull
        {
            var type = typeof(T);

            foreach (var (key, instance) in _instances)
            {
                if (key.Type == type)
                {
                    value = (T)instance;

                    return true;
                }
            }

            value = default;

            return false;
        }

        public bool TryGet<T>(string? key, out T? value)
            where T : notnull
        {
            if (_instances.TryGetValue(typeof(T).CreateKey(key), out var instance))
            {
                value = (T)instance;

                return true;
            }

            value = default;

            return false;
        }
    }
}
