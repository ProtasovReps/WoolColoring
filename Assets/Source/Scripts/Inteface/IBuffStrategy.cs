public interface IBuffStrategy
{
    int Price { get; }

    void Execute();
    bool Validate();
}
