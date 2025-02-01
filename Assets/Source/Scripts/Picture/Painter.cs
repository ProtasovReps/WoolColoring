using System;
using UnityEngine;

public class Painter : MonoBehaviour
{
    //[SerializeField] private Kartinka
    [SerializeField] private ColoredStringHolderStash _holderStash;
    [SerializeField] private ColoredStringHolderSwitcher _switcher;

    private void Awake()
    {
        if (_holderStash == null)
            throw new NullReferenceException(nameof(_holderStash));
    }

    private void OnEnable()
    {
        foreach (IFillable<ColoredStringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled += OnFilled;
    }

    private void OnDisable()
    {
        foreach (IFillable<ColoredStringHolder> holder in _holderStash.ColoredStringHolders)
            holder.Filled -= OnFilled;
    }

    private void OnFilled(ColoredStringHolder holder)
    {
        FillImage(holder);
    }

    private void FillImage(ColoredStringHolder holder)
    {
        // Логика заполнения картинки...


        // Холдер исчерпан
        Color requiredColor = Color.black; //Получить рандомный необходимый цвет у картинки
        _switcher.Switch(requiredColor, holder);
    }
}
