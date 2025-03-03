namespace DynamicSun.Weather.Domain.Base
{
    public abstract class PersistentObjectT<TKey> where TKey : struct, IEquatable<TKey>
    {
        public TKey Id { get; set; }
        public DateTime ObjectCreateDate { get; set; } = DateTime.UtcNow;
        public DateTime ObjectEditDate { get; set; } = DateTime.UtcNow;
    }
}
