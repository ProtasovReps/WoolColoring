using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FigureBin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out FigureView fallable) == false)
            return;

        fallable.Fall();
    }
}
