using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FigureBin : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out FigureView fallable) == false)
            return;

        fallable.Fall();
    }
}