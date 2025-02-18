using System.Collections;
using UnityEngine;

public class EffectsPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _effect;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _effect = Instantiate(_effect);

        _effect.gameObject.SetActive(false);
    }

    public void Play() => StartCoroutine(PlayEffect());

    private IEnumerator PlayEffect()
    {
        _effect.transform.position = _transform.position;

        _effect.gameObject.SetActive(true);
        _effect.Play();
        yield return new WaitForSeconds(_effect.main.duration);

        _effect.gameObject.SetActive(false);
    }
}
