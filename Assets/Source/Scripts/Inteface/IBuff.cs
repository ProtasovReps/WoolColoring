public interface IBuff
{
    int Price { get; }

    void Execute();
    bool Validate();
}
