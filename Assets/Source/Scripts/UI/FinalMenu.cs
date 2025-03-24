using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UnityEngine;

public class FinalMenu : ActivatableUI
{
    [SerializeField] private SoundID _soundID;
    [SerializeField] private float _activateDelay;

    private BoltClickReader _boltClickReader;
    private FigureClickReader _figureClickReader;
    private Picture _picture;

    [Inject]
    private void Inject(Picture picture, BoltClickReader boltClickReader, FigureClickReader figureClickReader)
    {
        _boltClickReader = boltClickReader;
        _figureClickReader = figureClickReader;
        _picture = picture;
        _picture.Colorized += () => ActivateFinalMenu().Forget();
    }

    private void OnDestroy() => Unsubscribe();

    private async UniTaskVoid ActivateFinalMenu()
    {
        Unsubscribe();

        _figureClickReader.SetPause(true);
        _boltClickReader.SetPause(true);

        await UniTask.WaitForSeconds(_activateDelay);
        BroAudio.Stop(BroAudioType.Music);
        BroAudio.Play(_soundID);

        Activate();
    }

    private void Unsubscribe()
    {
        _picture.Colorized -= () => ActivateFinalMenu().Forget();
    }
}