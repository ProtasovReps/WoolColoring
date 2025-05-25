namespace CustomInterface
{
    public interface IBuff
    {
        string Id { get; }
        int Price { get; }

        void Execute();
        bool Validate();
    }
}
