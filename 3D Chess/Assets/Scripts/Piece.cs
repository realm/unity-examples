using UnityEngine;

public class Piece : MonoBehaviour
{
    public PieceType type = default;

    private Events events = default;
    private readonly Color selectedColor = new Color(1, 0, 0, 1);
    private readonly Color deselectedColor = new Color(1, 1, 1, 1);

    public void Select()
    {
        gameObject.GetComponent<Renderer>().material.color = selectedColor;
    }

    public void Deselect()
    {
        gameObject.GetComponent<Renderer>().material.color = deselectedColor;
    }

    private void OnMouseDown()
    {
        events.PieceClickedEvent.Invoke(this);
    }

    private void Awake()
    {
        events = FindObjectOfType<Events>();
    }
}
