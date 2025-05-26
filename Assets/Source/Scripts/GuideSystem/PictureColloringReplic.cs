using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UnityEngine;
using ClickReaders;
using BlockPicture.Model;

namespace PlayerGuide
{
    public class PictureColloringReplic : Replic
    {
        [SerializeField] private GuideBoltClickReader _guideBoltClickReader;
        [SerializeField] private ReplicPlayer _player;
        [SerializeField] private float _deactivateDelay;

        private Picture _picture;

        [Inject]
        private void Inject(Picture picture)
        {
            _picture = picture;
            _picture.BlockCountChanged += () => OnBlockColorChanged().Forget();
        }

        public override void Activate()
        {
            _guideBoltClickReader.SetPause(false);
            _guideBoltClickReader.SetWhiteStringHolderGuide(false);
            base.Activate();
        }

        private async UniTaskVoid OnBlockColorChanged()
        {
            _guideBoltClickReader.SetPause(true);
            _player.transform.gameObject.SetActive(false);
            await UniTask.WaitForSeconds(_deactivateDelay);
            Deactivate();
        }
    }
}