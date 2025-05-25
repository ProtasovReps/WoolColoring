using Bolts.View;
using Cysharp.Threading.Tasks;
using FigurePlatform.View;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace PlayerGuide
{
    public class BoltPointer : MonoBehaviour
    {
        [SerializeField] private Pointer[] _pointers;
        [SerializeField] private float _appearDelay = 1f;

        private FigureCompositionView _figureCompositionView;
        private List<FigureView> _figureViews;
        private int _lastFigure = 0;

        private void Start()
        {
            Initialize().Forget();
        }

        private async UniTaskVoid Initialize()
        {
            await UniTask.WaitForSeconds(_appearDelay);

            _figureCompositionView = GetComponentInChildren<FigureCompositionView>();

            if (_figureCompositionView == null)
                throw new NullReferenceException(nameof(_figureCompositionView));

            _figureViews = GetList(_figureCompositionView.FigureViews);
            ShowPointers();
        }

        private void ShowPointers()
        {
            for (int i = 0; i < _pointers.Length; i++)
            {
                Bolt bolt = GetRandomBolt();
                Pointer pointer = Instantiate(_pointers[i]);

                pointer.SetTarget(bolt.Transform);
            }
        }

        private Bolt GetRandomBolt()
        {
            if (_lastFigure >= _figureViews.Count)
                throw new ArgumentOutOfRangeException();

            FigureView figureView = _figureViews[_lastFigure];
            List<Bolt> bolts = GetList(figureView.Bolts);
            Bolt randomBolt = GetRandomCharacter(bolts);

            _lastFigure++;
            return randomBolt;
        }

        private T GetRandomCharacter<T>(List<T> characters)
        {
            int randomCharacterIndex = Random.Range(0, characters.Count);
            return characters[randomCharacterIndex];
        }

        private List<T> GetList<T>(IEnumerable<T> enumerable)
        {
            List<T> objects = new();

            foreach (T newObject in enumerable)
            {
                objects.Add(newObject);
            }

            return objects;
        }
    }
}