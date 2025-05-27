using System;
using UnityEngine;

namespace FigurePlatformSystem.Model
{
    public class Figure
    {
        public event Action Appeared;
        public event Action<Color> ColorChanged;
        public event Action Falled;

        public void SetColor(Color color)
        {
            ColorChanged?.Invoke(color);
        }

        public void Appear()
        {
            Appeared?.Invoke();
        }

        public void Fall()
        {
            Falled?.Invoke();
        }
    }
}