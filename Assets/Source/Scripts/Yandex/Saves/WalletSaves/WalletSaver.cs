using CustomInterface;
using YG;
using PlayerWallet;

namespace YandexGamesSDK.Saves.PlayerWallet
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
