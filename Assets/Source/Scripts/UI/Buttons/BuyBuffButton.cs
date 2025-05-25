using Ami.BroAudio;
using CustomInterface;
using Extensions;
using LevelInterface.Blocks;
using Reflex.Attributes;
using TMPro;
using UnityEngine;
using YG;

namespace LevelInterface.Buttons
{
    public abstract class BuyBuffButton<T> : ButtonView, IBuffBuyButton<T> where T : IBuff
    {
        [SerializeField] private SoundID _purchaseRejectedSound;
        [SerializeField] private ParticleSystem _purchaseEffect;
        [SerializeField] private TMP_Text _priceText;
        [SerializeField, Min(1)] private int _buffBuyCount;
        [SerializeField] private MetricParams _metricParams;

        private Store _store;
        private T _buff;

        public T CurrentBuff => _buff;

        [Inject]
        private void Inject(Store store, T buff)
        {
            _store = store;
            _buff = buff;
            _priceText.text = _buff.Price.ToString();
        }

        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }

        protected override void OnButtonClick()
        {
            if (_store.TryPurchase(_buff, _buffBuyCount) == false)
            {
                BroAudio.Play(_purchaseRejectedSound);
            }
            else
            {
                YG2.MetricaSend(_metricParams.ToString());
                _purchaseEffect.Play();
                base.OnButtonClick();
            }
        }
    }
}