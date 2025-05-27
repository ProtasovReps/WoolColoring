using System;
using UnityEngine;
using LitMotion;
using TMPro;

namespace GuideSystem
{
    public class ReplicAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration;

        public event Action Finalized;

        public void Activate(TMP_Text text)
        {
            LMotion.Create(0, text.text.Length, _duration)
                .WithOnComplete(() => Finalized?.Invoke())
                .Bind(x => text.maxVisibleCharacters = x);
        }
    }
}