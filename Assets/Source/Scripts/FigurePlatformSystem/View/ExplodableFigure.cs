using Ami.BroAudio;
using UnityEngine;

namespace FigurePlatform.View
{
    public class ExplodableFigure : MonoBehaviour
    {
        [SerializeField] private FigureView _figureView;
        [SerializeField] private SoundID _explosionSound;

        public void Explode()
        {
            _explosionSound.Play();
            _figureView.Fall();
        }
    }
}