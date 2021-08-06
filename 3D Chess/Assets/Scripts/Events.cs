using UnityEngine;
using UnityEngine.Events;

public class PieceClickedEvent : UnityEvent<Piece> { }
public class SquareClickedEvent : UnityEvent<Vector3> { }

public class Events : MonoBehaviour
{
    public readonly PieceClickedEvent PieceClickedEvent = new PieceClickedEvent();
    public readonly SquareClickedEvent SquareClickedEvent = new SquareClickedEvent();
}
