using UISystem.Timers;
using UnityEngine;

namespace UISystem.Buttons
{
    public class FinalCoinRewardButton : CoinAdRewardButton
    {
        [SerializeField] private float _cooldownTime = 5;

        private void Awake()
        {
            Initialize(new AdTimer(_cooldownTime));
        }

        protected override void OnButtonClick()
        {
            transform.gameObject.SetActive(false);
            base.OnButtonClick();
        }
    }
}