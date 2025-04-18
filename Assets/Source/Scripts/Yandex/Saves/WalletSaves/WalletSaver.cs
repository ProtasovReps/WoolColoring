using YG;

public class WalletSaver : ISaver
{
    private Wallet _wallet;

    public WalletSaver(Wallet wallet)
    {
        _wallet = wallet;
    }

    public void Save()
    {
        YG2.saves.Coins = _wallet.Count;
    }
}
