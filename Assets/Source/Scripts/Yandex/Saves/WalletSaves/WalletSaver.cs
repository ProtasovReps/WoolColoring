using Interface;
using YG;
using WalletSystem;

namespace Yandex.Saves.PlayerWallet
{
    public class WalletSaver : ISaver
    {
        private readonly Wallet _wallet;

        public WalletSaver(Wallet wallet)
        {
            _wallet = wallet;
        }

        public void Save()
        {
            YG2.saves.Coins = _wallet.Count;
        }
    }
}
