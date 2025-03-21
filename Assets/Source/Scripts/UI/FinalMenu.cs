using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UnityEngine;

public class FinalMenu : ActivatableUI
{
    [SerializeField] private SoundID _soundID;
    [SerializeField] private float _activateDelay;

    private Picture _picture;

    [Inject]
    private void Inject(Picture picture)
    {
        _picture = picture;
        _picture.Colorized += () => ActivateFinalMenu().Forget();
    }

    private void OnDestroy() => Unsubscribe();

    private async UniTaskVoid ActivateFinalMenu()
    {
        Unsubscribe();

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