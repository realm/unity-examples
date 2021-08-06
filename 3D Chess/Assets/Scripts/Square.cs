using UnityEngine;

public class Square : MonoBehaviour
{
    private Events events = default;

    private void OnMouseDown()
    {
        events.SquareClickedEvent.Invoke(transform.position);
    }

    private void Awake()
    {
        events = FindObjectOfType<Events>();
    }
}
