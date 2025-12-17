using StartupOrchestrator.Abstractions.Extensions;
using StartupOrchestrator.Abstractions.Synchronization;

namespace StartupOrchestrator.Synchronization
{
    public sealed class KeyedBarrierActionRegistry : IKeyedBarrierRegistry
    {
        private readonly Dictionary<RegistryKey, object> _instances = new();
        private readonly object _lock = new();
        private readonly List<IBarrierRegistration> _registrations = new();

        public void AddDependency(
            IBarrierRegistration registration,
            RegistryKey key,
            Action<IRegistryContext>? callback)
        {
            lock (_lock)
            {
                registration.Require(key);

                if (callback is null)
                    return;

                if (registration.Callback is not null)
                    throw new InvalidOperationException("Barrier callback already defined. Only one final callback is allowed.");

                registration.Callback = callback;

                TryExecuteRegistrationLocked(registration);
            }
        }

        public IBarrierBuilder Register<T>(string? key = null)
                    where T : notnull
        {
            var registration = new BarrierRegistration();
            registration.Require(typeof(T).GetRegistryKey(key));

            lock (_lock)
            {
                _registrations.Add(registration);
            }

            return new BarrierBuilder(this, registration);
        }

        public void RegisterCallback<T>(
            Action<IRegistryContext> callback,
            string? key = null)
            where T : notnull
        {
            Register<T>(key).RegisterCallback<T>(callback, key);
        }

        public void Trigger<T>(T instance, string? key = null)
            where T : notnull
        {
            lock (_lock)
            {
                _instances[typeof(T).GetRegistryKey(key)] = instance;
                TryExecuteLocked();
            }
        }

        private void TryExecuteLocked()
        {
            foreach (var registration in _registrations.Where(x => !x.Executed))
            {
                TryExecuteRegistrationLocked(registration);
            }
        }

        private void TryExecuteRegistrationLocked(IBarrierRegistration registration)
        {
            if (registration.Executed)
                return;

            if (!registration.Requirements.All(x => _instances.ContainsKey(x)))
                return;

            registration.Executed = true;

            var scopedInstances = registration.Requirements
                .ToDictionary(x => x, x => _instances[x]);

            registration.Callback?.Invoke(new RegistryContext(scopedInstances));
        }
    }
}
