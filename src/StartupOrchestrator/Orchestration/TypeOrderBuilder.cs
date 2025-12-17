namespace StartupOrchestrator.Orchestration
{
    public sealed class TypeOrderBuilder
    {
        private readonly List<Type> _types = new();

        public TypeOrderBuilder Add<T>()
        {
            _types.Add(typeof(T));

            return this;
        }

        public TypeOrderBuilder AddAfter<TAfter, TNew>()
        {
            var afterType = typeof(TAfter);
            var newType = typeof(TNew);

            var index = _types.IndexOf(afterType);
            if (index == -1)
            {
                throw new InvalidOperationException($"Type {afterType.Name} not found in order list.");
            }

            _types.Insert(index + 1, newType);

            return this;
        }

        public IReadOnlyList<Type> Build()
        {
            return _types.AsReadOnly();
        }
    }
}
