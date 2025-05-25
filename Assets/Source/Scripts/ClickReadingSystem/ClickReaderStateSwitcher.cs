using Reflex.Attributes;
using UnityEngine;

namespace ClickReaders
{
    public class ClickReaderStateSwitcher : MonoBehaviour
    {
        private BoltClickReader _boltClickReader;
        private FigureClickReader _figureClickReader;
        private bool _lastFigureClickReaderState;

        [Inject]
        private void Inject(BoltClickReader boltReader, FigureClickReader figureClickReader)
        {
            _boltClickReader = boltReader;
            _figureClickReader = figureClickReader;
        }

        private void OnEnable()
        {
            _lastFigureClickReaderState = _figureClickReader.IsPaused;

            _figureClickReader.SetPause(true);
            _boltClickReader.SetPause(true);
        }

        private void OnDisable()
        {
            _figureClickReader.SetPause(_lastFigureClickReaderState);
        }
    }
}