using Ami.BroAudio;
using Cysharp.Threading.Tasks;
using Reflex.Attributes;
using UnityEngine;

[RequireComponent (typeof(ActivatableUI))]
public class FinalMenu : MonoBehaviour
{
    [SerializeField] private SoundID _soundID;
    [SerializeField] private float _activateDelay;

    private ActivatableUI _activatable;
    private Picture _picture;

    [Inject]
    private void Inject(Picture picture)
    {
        _picture = picture;
        _activatable = GetComponent<ActivatableUI>();
        _picture.Colorized += () => Activate().Forget();
    }

    private void OnDestroy() => Unsubscribe();

    private async UniTaskVoid Activate()
    {
        Unsubscribe();

        await UniTask.WaitForSeconds(_activateDelay);
        BroAudio.Stop(BroAudioType.Music);
        BroAudio.Play(_soundID);

        _activatable.Activate();
    }

    private void Unsubscribe()
    {
        _picture.Colorized -= () => Activate().Forget();
    }
}