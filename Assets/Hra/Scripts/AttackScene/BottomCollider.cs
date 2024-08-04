using UnityEngine;

public class BottomCollider : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;

    public Collider Collider;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DiceSide"))
        {
            Collider = other;
        }
    }
}
