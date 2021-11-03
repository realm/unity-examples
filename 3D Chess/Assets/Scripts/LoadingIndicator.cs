using UnityEngine;

public class LoadingIndicator : MonoBehaviour
{
    // 1
    [SerializeField] private float maxMovement = 100;
    [SerializeField] private float speed = 200;

    // 2
    private Vector3 movementDirection = Vector3.left;

    private void Update()
    {
        transform.Translate(movementDirection * speed * Time.deltaTime);
        // 3
        if (Mathf.Abs(transform.localPosition.x) >= maxMovement)
        {
            // 4
            Mathf.Clamp(transform.localPosition.x, -maxMovement, maxMovement);
            // 5
            movementDirection = -movementDirection;
        }
    }
}