using System.Linq;
using UnityEngine;

public class GameState : MonoBehaviour
{
    [SerializeField] private PieceSpawner pieceSpawner = default;
    [SerializeField] private GameObject pieces = default;

    public void MovePiece(Piece movedPiece, Vector3 newPosition)
    {
        // Check if there is already a piece at the new position and if so, destroy it.
        var attackedPiece = FindPiece(newPosition);
        if (attackedPiece != null)
        {
            Destroy(attackedPiece.gameObject);
        }

        // Update the movedPiece's GameObject.
        movedPiece.transform.position = newPosition;
    }

    public void ResetGame()
    {
        // Destroy all GameObjects.
        foreach (var piece in pieces.GetComponentsInChildren<Piece>())
        {
            Destroy(piece.gameObject);
        }

        // Recreate the GameObjects.
        pieceSpawner.CreateGameObjects(pieces);
    }

    private void Awake()
    {
        pieceSpawner.CreateGameObjects(pieces);
    }

    private Piece FindPiece(Vector3 position)
    {
        return pieces.GetComponentsInChildren<Piece>()
            .FirstOrDefault(piece => piece.transform.position == position);
    }
}
