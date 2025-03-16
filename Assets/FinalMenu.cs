using Ami.BroAudio;
using Reflex.Attributes;
using UnityEngine;

[RequireComponent (typeof(ActivatableUI))]
public class FinalMenu : MonoBehaviour
{
    [SerializeField] private SoundID _soundID;

    private ActivatableUI _activatable;
    private Picture _picture;

    [Inject]
    private void Inject(Picture picture)
    {
        _picture = picture;
        _activatable = GetComponent<ActivatableUI>();
        _picture.Colorized += Activate;
    }

    private void OnDestroy() => Unsubscribe();

    private void Activate()
    {
        Unsubscribe();

        BroAudio.Stop(BroAudioType.Music);
        BroAudio.Play(_soundID);

        _activatable.Activate();
    }

    private void Unsubscribe()
    {
        _picture.Colorized -= Activate;
    }
}