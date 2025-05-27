using MolbertSystem.Model;
using Cysharp.Threading.Tasks;
using UISystem.Buttons;
using Reflex.Attributes;
using UnityEngine;
using YG;

namespace GuideSystem
{
    public class GuideLevelFinalizer : MonoBehaviour
    {
        [SerializeField] private SwitchSceneButton _switchButton;
        [SerializeField] private float _switchSceneDelay;

        private Picture _picture;

        [Inject]
        private void Inject(Picture picture)
        {
            _picture = picture;
            _picture.Finished += () => OnFinished().Forget();
        }

        private void OnDestroy()
        {
            _picture.Finished -= () => OnFinished().Forget();
        }

        private async UniTaskVoid OnFinished()
        {
            await UniTask.WaitForSeconds(_switchSceneDelay);
            YG2.SkipNextInterAdCall();
            _switchButton.Switch();
        }
    }
}