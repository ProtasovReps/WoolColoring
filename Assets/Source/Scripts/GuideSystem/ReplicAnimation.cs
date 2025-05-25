using System;
using UnityEngine;
using LitMotion;
using TMPro;

namespace PlayerGuide
{
    public class ReplicAnimation : MonoBehaviour
    {
        [SerializeField] private float _duration;

        public void Activate(TMP_Text text, Action callback)
        {
            LMotion.Create(0, text.text.Length, _duration)
                .WithOnComplete(callback)
                .Bind(x => text.maxVisibleCharacters = x);
        }
    }
}