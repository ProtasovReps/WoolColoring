using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FigureBin : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.gameObject.SetActive(false);
    }
}
