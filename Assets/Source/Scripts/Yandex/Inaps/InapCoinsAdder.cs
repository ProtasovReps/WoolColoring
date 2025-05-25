using PlayerWallet;

namespace YandexGamesSDK.Inaps
{
    public class InapCoinsAdder
    {
        private readonly Wallet _wallet;

        public InapCoinsAdder(Wallet wallet)
        {
            _wallet = wallet;
        }

        public void AddCoins(int amount)
        {
            _wallet.Add(amount);
        }
    }
}