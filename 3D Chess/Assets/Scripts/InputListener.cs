using UnityEngine;

public class InputListener : MonoBehaviour
{
    [SerializeField] private Events events = default;
    [SerializeField] private GameState gameState = default;

    private Piece activePiece = default;

    private void OnEnable()
    {
        events.PieceClickedEvent.AddListener(OnPieceClicked);
        events.SquareClickedEvent.AddListener(OnSquareClicked);
    }

    private void OnDisable()
    {
        events.PieceClickedEvent.RemoveListener(OnPieceClicked);
        events.SquareClickedEvent.RemoveListener(OnSquareClicked);
    }

    private void OnPieceClicked(Piece piece)
    {
        if (activePiece != null)
        {
            activePiece.Deselect();
        }
        activePiece = piece;
        activePiece.Select();
    }

    private void OnSquareClicked(Vector3 position)
    {
        if (activePiece != null)
        {
            gameState.MovePiece(activePiece, position);
            activePiece.Deselect();
            activePiece = null;
        }
    }
}
