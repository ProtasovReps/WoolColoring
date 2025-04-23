public class InapBuffAdder
{
    private readonly Wallet _wallet;
    private readonly Store _store;
    private readonly IBuff[] _buffs;

    public InapBuffAdder(Wallet wallet, Store store, params IBuff[] buffs)
    {
        _wallet = wallet;
        _store = store;
        _buffs = buffs;
    }

    public void AddBuffs(int amount)
    {
        for (int i = 0; i < _buffs.Length; i++)
        {
            _wallet.AddSilent(_buffs[i].Price * amount);
            _store.TryPurchase(_buffs[i], amount);
        }
    }
}