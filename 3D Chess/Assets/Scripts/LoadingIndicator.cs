using UnityEngine;

public class LoadingIndicator : MonoBehaviour
{
    [SerializeField] private float maxLeft = 0;
    [SerializeField] private float maxRight = 0;
    [SerializeField] private float speed = 100;

    private enum MovementDirection { None, Left, Right }
    private MovementDirection movementDirection = MovementDirection.Left;

    private void Update()
    {
        switch (movementDirection)
        {
            case MovementDirection.None:
                break;
            case MovementDirection.Left:
                transform.Translate(speed * Time.deltaTime * Vector3.left);
                if (transform.localPosition.x <= maxLeft)
                {
                    transform.localPosition = new Vector3(maxLeft, transform.localPosition.y, transform.localPosition.z);
                    movementDirection = MovementDirection.Right;
                }
                break;
            case MovementDirection.Right:
                transform.Translate(speed * Time.deltaTime * Vector3.right);
                if (transform.localPosition.x >= maxRight)
                {
                    transform.localPosition = new Vector3(maxRight, transform.localPosition.y, transform.localPosition.z);
                    movementDirection = MovementDirection.Left;
                }
                break;
        }
    }
}
