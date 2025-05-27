using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Extensions.View;

namespace MolbertSystem.View
{
    public class PictureTransformView : TransformView
    {
        private CancellationTokenSource _cancellationTokenSource;

        public void ChangePosition(Transform comparer, Vector3 targetPosition, float moveSpeed)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();

            MoveSmoothly(comparer, targetPosition, moveSpeed).Forget();
        }

        private async UniTaskVoid MoveSmoothly(Transform comparer, Vector3 targetPosition, float moveSpeed)
        {
            _cancellationTokenSource = new CancellationTokenSource();

            Vector3 offset;
            Vector3 finalPosition;

            while (Mathf.Approximately(targetPosition.y, comparer.position.y) == false)
            {
                offset = targetPosition - comparer.position;
                finalPosition = Transform.position + new Vector3(0f, offset.y, 0f);

                Transform.position = Vector3.Lerp(Transform.position, finalPosition, moveSpeed * Time.deltaTime);
                await UniTask.Yield(PlayerLoopTiming.Update, _cancellationTokenSource.Token);
            }
        }
    }
}