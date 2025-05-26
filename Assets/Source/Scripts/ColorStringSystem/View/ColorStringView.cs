using System;
using UnityEngine;
using LitMotion;
using LitMotion.Extensions;
using ColorStrings.Presenter;
using Extensions;
using Extensions.View;

namespace ColorStrings.View
{
    [RequireComponent(typeof(ColorView))]
    [RequireComponent(typeof(TransformView))]
    [RequireComponent(typeof(ActiveStateSwitcher))]
    public class ColorStringView : MonoBehaviour
    {
        [SerializeField] private float _appearDuration = 0.2f;
        [SerializeField] private float _disappearDuration = 1f;

        private ColorStringPresenter _presenter;
        private ActionQueue _actionQueue;
        private ColorView _colorView;
        private TransformView _transformView;
        private ActiveStateSwitcher _stateSwitcher;

        public bool IsAnimating => _actionQueue.IsAnimating;
        public Transform Transform => _transformView.Transform;

        public void Initialize(ColorStringPresenter presenter)
        {
            if (presenter == null)
                throw new ArgumentNullException(nameof(presenter));

            _presenter = presenter;
            _actionQueue = new ActionQueue();
            _stateSwitcher = GetComponent<ActiveStateSwitcher>();
            _transformView = GetComponent<TransformView>();
            _colorView = GetComponent<ColorView>();

            _transformView.Initialize();
            _colorView.Initialize();
            _stateSwitcher.Initialize();
        }

        public void Appear()
        {
            _actionQueue.AddAction(AppearAnimated);
            _actionQueue.ValidateAction();
        }

        public void Disappear()
        {
            _actionQueue.AddAction(DisappearAnimated);
            _actionQueue.ValidateAction();
        }

        private void AppearAnimated()
        {
            Color newColor = _presenter.GetColor();

            _colorView.SetColor(newColor);
            _stateSwitcher.SetActive(true);

            LMotion.Create(Vector3.zero, _transformView.StartScale, _appearDuration)
                .WithOnComplete(_actionQueue.ProcessQueuedAction)
                .BindToLocalScale(_transformView.Transform);
        }

        private void DisappearAnimated()
        {
            LMotion.Create(_transformView.Transform.localScale, Vector3.zero, _disappearDuration)
                .WithOnComplete(FinalizeDisappearing)
                .BindToLocalScale(_transformView.Transform);
        }

        private void FinalizeDisappearing()
        {
            _stateSwitcher.SetActive(false);
            _actionQueue.ProcessQueuedAction();
        }
    }
}