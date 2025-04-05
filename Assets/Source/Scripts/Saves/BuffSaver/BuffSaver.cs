using System;

public abstract class BuffSaver : IDisposable
{
    private readonly BuffBag _bag;

    public BuffSaver(BuffBag bag)
    {
        if (bag == null)
            throw new ArgumentNullException(nameof(bag));

        _bag = bag;
        _bag.AmountChanged += Save;
    }

    protected BuffBag BuffBag => _bag;

    public void Dispose() => _bag.AmountChanged -= Save;

    public abstract void Save(IBuff buff);
}