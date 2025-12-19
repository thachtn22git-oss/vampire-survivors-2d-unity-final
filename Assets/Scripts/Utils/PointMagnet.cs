using UnityEngine;

public class PointMagnet : MonoBehaviour
{
    [SerializeField] private float pullSpeed = 10f;

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Point"))
        {
            other.transform.position = Vector3.MoveTowards(
                other.transform.position,
                transform.position,
                pullSpeed * Time.deltaTime
            );
        }
    }
}
