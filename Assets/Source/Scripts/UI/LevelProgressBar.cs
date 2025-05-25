using Reflex.Attributes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using BlockPicture.Model;

namespace LevelInterface
{
    public class LevelProgressBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _text;

        private Picture _picture;
        private int _maxBlockCount;

        [Inject]
        private void Inject(Picture picture)
        {
            _picture = picture;
            _maxBlockCount = _picture.UncoloredBlocksCount;
            _picture.BlockCountChanged += OnBlockCountChanged;

            OnBlockCountChanged();
        }

        private void OnDestroy()
        {
            _picture.BlockCountChanged -= OnBlockCountChanged;
        }

        private void OnBlockCountChanged()
        {
            float targetValue = 1f - (float)_picture.UncoloredBlocksCount / _maxBlockCount;

            _slider.value = targetValue;
            SetPercentValue();
        }

        private void SetPercentValue()
        {
            int percentValue = (int)(_slider.value / _slider.maxValue * 100);
            string percent = $"{percentValue}%";

            _text.text = percent;
        }
    }
}