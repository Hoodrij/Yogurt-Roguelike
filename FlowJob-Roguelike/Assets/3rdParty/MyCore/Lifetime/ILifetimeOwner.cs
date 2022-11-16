namespace Core.Tools
{
    public interface ILifetimeOwner
    {
        internal Lifetime Lifetime { get; }
    }
}