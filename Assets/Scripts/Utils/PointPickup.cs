using UnityEngine;

public class PointPickup : MonoBehaviour
{
    [SerializeField] private int pointValue = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerController.Instance.AddPoint(pointValue);
            Destroy(gameObject);
        }
    }
}
