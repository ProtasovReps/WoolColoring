using LevelInterface.Buttons;
using PlayerWallet;
using Reflex.Attributes;
using UnityEngine;
using YG;

namespace YandexGamesSDK
{
    public class AcceptGameLabelButton : ButtonView
    {
        [SerializeField] private GameLabelYG _gameLabelYG;
        [SerializeField, Min(0)] private int _addAmount;

        private Wallet _wallet;

        [Inject]
        private void Inject(Wallet wallet)
        {
            _wallet = wallet;
        }

        private void OnEnable()
        {
            _gameLabelYG.onPromptSuccess.AddListener(AddMoney);
        }

        private void OnDisable()
        {
            _gameLabelYG.onPromptSuccess.RemoveListener(AddMoney);
        }

        protected override void OnButtonClick()
        {
            _gameLabelYG.GameLabelShowDialog();
            base.OnButtonClick();
        }

        private void AddMoney()
        {
            _wallet.Add(_addAmount);
        }
    }
}