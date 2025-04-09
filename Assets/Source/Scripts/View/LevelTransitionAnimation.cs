using LitMotion;
using LitMotion.Extensions;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(SpriteRenderer))]
public class LevelTransitionAnimation : MonoBehaviour
{
    [SerializeField] private float _startScale = 10f;
    [SerializeField] private float _endScale = 0f;
    [SerializeField] private float _transitionSpeed;
    [SerializeField] private Color[] _colors;

    private Transform _transform;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _transform = transform;
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    [ContextMenu("in")]
    public void FadeIn()
    {
        LMotion.Create(Vector3.one * _startScale, Vector3.one * _endScale, _transitionSpeed)
            .WithEase(Ease.InOutCirc)
            .BindToLocalScale(_transform);
    }

    [ContextMenu("out")]
    public void FadeOut(Action callback)
    {
        var randomIndex = Random.Range(0, _colors.Length);
        _spriteRenderer.color = _colors[randomIndex];

        LMotion.Create(Vector3.one * _endScale, Vector3.one * _startScale, _transitionSpeed)
            .WithEase(Ease.InOutCirc)
            .WithOnComplete(callback)
            .BindToLocalScale(_transform);
    }
}