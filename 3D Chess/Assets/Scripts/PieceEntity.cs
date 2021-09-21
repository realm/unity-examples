using MongoDB.Bson;
using Realms;
using UnityEngine;

public class PieceEntity : RealmObject
{
    [PrimaryKey]
    [MapTo("_id")]
    public ObjectId Id { get; set; } = ObjectId.GenerateNewId();

    // An `enum` cannot be saved directly in Realm, we need to use a type that can be saved which in this case is an `int`.
    public PieceType PieceType
    {
        get => (PieceType)Type;
        private set => Type = (int)value;
    }

    // Custom types like `Vector3` need to be represented by a type that can be saved in Realm.
    // We create a `RealmObject` for this (`Vector3Entity`) that holds the three values of the vector.
    public Vector3 Position
    {
        get => PositionEntity.ToVector3();
        set => PositionEntity = new Vector3Entity(value);
    }

    private int Type { get; set; }
    private Vector3Entity PositionEntity { get; set; }

    public PieceEntity(PieceType type, Vector3 position)
    {
        PieceType = type;
        Position = position;
    }

    // Because we have to use the `PositionEntity` internally but expose `Position` we also need to make sure every change
    // in `PositionEntity` leads to a notification listeners of `Position`.
    protected override void OnPropertyChanged(string propertyName)
    {
        if (propertyName == nameof(PositionEntity))
        {
            RaisePropertyChanged(nameof(Position));
        }
    }

    // A default constructor is mandatory for `RealmObject`s but can be `private` if it is not to be used.
    private PieceEntity()
    {
    }
}
