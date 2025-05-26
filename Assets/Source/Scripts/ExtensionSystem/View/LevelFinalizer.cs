using Ami.BroAudio;
using BlockPicture.Model;
using BlockPicture.View;
using ClickReaders;
using Cysharp.Threading.Tasks;
using Extensions;
using Input;
using LevelInterface;
using LitMotion;
using LitMotion.Extensions;
using Reflex.Attributes;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Extensions.View
{
    public class LevelFinalizer : MonoBehaviour
    {
        [SerializeField] private SoundID _finalMusic;
        [SerializeField] private ActivatableUI _finalBlock;
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private PictureView _pictureView;
        [SerializeField] private ParticleSystem[] _effects;
        [SerializeField] private Canvas[] _levelUI;
        [SerializeField] private float _cameraTranslateDuration = 1.5f;
        [SerializeField] private float _cameraUpTargetPosition = 5f;
        [SerializeField] private float _finalMenuAppearDelay;

        private PlayerInput _playerInput;
        private Picture _picture;
        private BoltClickReader _boltReader;
        private FigureClickReader _figureReader;

        [Inject]
        private void Inject(Picture picture, BoltClickReader boltClickReader, FigureClickReader figureClickReader, PlayerInput input)
        {
            _picture = picture;
            _boltReader = boltClickReader;
            _figureReader = figureClickReader;
            _picture.Finished += () => Finalize().Forget();
            _playerInput = input;
        }

        private async UniTaskVoid Finalize()
        {
            DisableUI();
            DisableClickReaders();
            AnimateCamera();
            PlayEffects();
            SwitchMusic();
            SendMetrics();

            _pictureView.ResetPosition();
            await UniTask.WaitForSeconds(_finalMenuAppearDelay);
            _finalBlock.Activate();
            _playerInput.PlayerClick.Disable();
        }

        private void DisableUI()
        {
            for (int i = 0; i < _levelUI.Length; i++)
                _levelUI[i].gameObject.SetActive(false);
        }

        private void DisableClickReaders()
        {
            _boltReader.SetPause(true);
            _figureReader.SetPause(true);
        }

        private void AnimateCamera()
        {
            Vector3 position = _mainCamera.transform.position;

            LMotion.Create(position, position + Vector3.up * _cameraUpTargetPosition, _cameraTranslateDuration)
                .WithEase(Ease.InOutElastic)
                .BindToPosition(_mainCamera.transform);
        }

        private void PlayEffects()
        {
            for (int i = 0; i < _effects.Length; i++)
                _effects[i].Play();
        }

        private void SwitchMusic()
        {
            BroAudio.Stop(BroAudioType.Music);
            BroAudio.Play(_finalMusic);
        }

        private void SendMetrics()
        {
            int levelIndex = SceneManager.GetActiveScene().buildIndex;
            string metricsFormatedId = $"{levelIndex}{MetricParams.LevelPassed}";

            YG2.MetricaSend(metricsFormatedId);
        }
    }
}