using Realms;
using UnityEngine;

public class PieceSpawner : MonoBehaviour
{
    [SerializeField] private Piece prefabBlackBishop = default;
    [SerializeField] private Piece prefabBlackKing = default;
    [SerializeField] private Piece prefabBlackKnight = default;
    [SerializeField] private Piece prefabBlackPawn = default;
    [SerializeField] private Piece prefabBlackQueen = default;
    [SerializeField] private Piece prefabBlackRook = default;

    [SerializeField] private Piece prefabWhiteBishop = default;
    [SerializeField] private Piece prefabWhiteKing = default;
    [SerializeField] private Piece prefabWhiteKnight = default;
    [SerializeField] private Piece prefabWhitePawn = default;
    [SerializeField] private Piece prefabWhiteQueen = default;
    [SerializeField] private Piece prefabWhiteRook = default;

    public void CreateNewBoard(Realm realm)
    {
        realm.Write(() =>
        {
            realm.RemoveAll<PieceEntity>();

            realm.Add(new PieceEntity(PieceType.WhiteRook, new Vector3(1, 0, 1)));
            realm.Add(new PieceEntity(PieceType.WhiteKnight, new Vector3(2, 0, 1)));
            realm.Add(new PieceEntity(PieceType.WhiteBishop, new Vector3(3, 0, 1)));
            realm.Add(new PieceEntity(PieceType.WhiteQueen, new Vector3(4, 0, 1)));
            realm.Add(new PieceEntity(PieceType.WhiteKing, new Vector3(5, 0, 1)));
            realm.Add(new PieceEntity(PieceType.WhiteBishop, new Vector3(6, 0, 1)));
            realm.Add(new PieceEntity(PieceType.WhiteKnight, new Vector3(7, 0, 1)));
            realm.Add(new PieceEntity(PieceType.WhiteRook, new Vector3(8, 0, 1)));

            realm.Add(new PieceEntity(PieceType.WhitePawn, new Vector3(1, 0, 2)));
            realm.Add(new PieceEntity(PieceType.WhitePawn, new Vector3(2, 0, 2)));
            realm.Add(new PieceEntity(PieceType.WhitePawn, new Vector3(3, 0, 2)));
            realm.Add(new PieceEntity(PieceType.WhitePawn, new Vector3(4, 0, 2)));
            realm.Add(new PieceEntity(PieceType.WhitePawn, new Vector3(5, 0, 2)));
            realm.Add(new PieceEntity(PieceType.WhitePawn, new Vector3(6, 0, 2)));
            realm.Add(new PieceEntity(PieceType.WhitePawn, new Vector3(7, 0, 2)));
            realm.Add(new PieceEntity(PieceType.WhitePawn, new Vector3(8, 0, 2)));

            realm.Add(new PieceEntity(PieceType.BlackPawn, new Vector3(1, 0, 7)));
            realm.Add(new PieceEntity(PieceType.BlackPawn, new Vector3(2, 0, 7)));
            realm.Add(new PieceEntity(PieceType.BlackPawn, new Vector3(3, 0, 7)));
            realm.Add(new PieceEntity(PieceType.BlackPawn, new Vector3(4, 0, 7)));
            realm.Add(new PieceEntity(PieceType.BlackPawn, new Vector3(5, 0, 7)));
            realm.Add(new PieceEntity(PieceType.BlackPawn, new Vector3(6, 0, 7)));
            realm.Add(new PieceEntity(PieceType.BlackPawn, new Vector3(7, 0, 7)));
            realm.Add(new PieceEntity(PieceType.BlackPawn, new Vector3(8, 0, 7)));

            realm.Add(new PieceEntity(PieceType.BlackRook, new Vector3(1, 0, 8)));
            realm.Add(new PieceEntity(PieceType.BlackKnight, new Vector3(2, 0, 8)));
            realm.Add(new PieceEntity(PieceType.BlackBishop, new Vector3(3, 0, 8)));
            realm.Add(new PieceEntity(PieceType.BlackQueen, new Vector3(4, 0, 8)));
            realm.Add(new PieceEntity(PieceType.BlackKing, new Vector3(5, 0, 8)));
            realm.Add(new PieceEntity(PieceType.BlackBishop, new Vector3(6, 0, 8)));
            realm.Add(new PieceEntity(PieceType.BlackKnight, new Vector3(7, 0, 8)));
            realm.Add(new PieceEntity(PieceType.BlackRook, new Vector3(8, 0, 8)));
        });
    }

    public void SpawnPiece(PieceEntity pieceEntity, GameObject parent)
    {
        var piecePrefab = pieceEntity.PieceType switch
        {
            PieceType.BlackBishop => prefabBlackBishop,
            PieceType.BlackKing => prefabBlackKing,
            PieceType.BlackKnight => prefabBlackKnight,
            PieceType.BlackPawn => prefabBlackPawn,
            PieceType.BlackQueen => prefabBlackQueen,
            PieceType.BlackRook => prefabBlackRook,
            PieceType.WhiteBishop => prefabWhiteBishop,
            PieceType.WhiteKing => prefabWhiteKing,
            PieceType.WhiteKnight => prefabWhiteKnight,
            PieceType.WhitePawn => prefabWhitePawn,
            PieceType.WhiteQueen => prefabWhiteQueen,
            PieceType.WhiteRook => prefabWhiteRook,
            _ => throw new System.Exception("Invalid piece type.")
        };

        var piece = Instantiate(piecePrefab, pieceEntity.Position, Quaternion.identity, parent.transform);
        piece.Entity = pieceEntity;
    }
}
