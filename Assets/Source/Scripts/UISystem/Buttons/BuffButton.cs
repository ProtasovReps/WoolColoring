using UnityEngine;
using System;
using Reflex.Attributes;
using Cysharp.Threading.Tasks;
using UnityEngine.UI;
using System.Threading;
using YG;
using Extensions;
using Buffs;
using LevelInterface.Blocks;
using CustomInterface;

namespace LevelInterface.Buttons
{
    public class BuffButton : ButtonView
    {
        [SerializeField] private BuffDealMenu _buffDealMenu;
        [SerializeField] private TemporaryActivatableUI _ruleText;
        [SerializeField] private BuffCountCounter _counter;
        [SerializeField] private float _coolDownTime;
        [SerializeField] private Image _cooldownImage;
        [SerializeField] private ParticleSystem _effect;
        [SerializeField] private MetricParams _metricParams;

        private BuffBag _bag;
        private IBuff _buff;
        private CancellationTokenSource _cancellationTokenSource;

        public bool IsNotCooldowning { get; private set; }

        [Inject]
        private void Inject(BuffBag buffBag)
        {
            _bag = buffBag;
            _counter.Initialize(_buff);
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
        }

        public void Initialize(IBuff buff)
        {
            if (buff == null)
                throw new ArgumentNullException(nameof(buff));

            _ruleText.Initialize();
            _buff = buff;
        }

        public void SetIsNotCooldowning(bool hasCooldown)
        {
            IsNotCooldowning = hasCooldown;
        }

        protected override void OnButtonClick()
        {
            base.OnButtonClick();

            if (_buff.Validate() == false)
            {
                _ruleText.Activate();
                return;
            }

            if (_bag.TryGetBuff(_buff) == false)
            {
                _buffDealMenu.SetTargetReward(_buff);
                _buffDealMenu.Activate();
                return;
            }

            _effect.Play();

            if (IsNotCooldowning == false)
            {
                WaitCoolDown().Forget();
            }

            YG2.MetricaSend(_metricParams.ToString());
            _buff.Execute();
        }

        private async UniTaskVoid WaitCoolDown()
        {
            _cancellationTokenSource = new CancellationTokenSource();
            Deactivate();

            float elapsedTime = 0f;

            while (elapsedTime < _coolDownTime)
            {
                _cooldownImage.fillAmount = 1 - (elapsedTime / _coolDownTime);

                elapsedTime += Time.deltaTime;
                await UniTask.Yield(cancellationToken: _cancellationTokenSource.Token, cancelImmediately: true);
            }

            _cooldownImage.fillAmount = 0f;
            Activate();
        }
    }
}