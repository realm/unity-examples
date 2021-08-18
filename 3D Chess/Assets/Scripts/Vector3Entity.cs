using Realms;
using UnityEngine;

// Every instance of a `Vector3Entity` is owned by exactly one `PieceEntity`.
// We can therefore use `EmbeddedObject` which is a special type of `RealmObject` enforcing this 1:1 relationship.
public class Vector3Entity : EmbeddedObject
{
    public float X { get; private set; }
    public float Y { get; private set; }
    public float Z { get; private set; }

    public Vector3Entity(Vector3 vector)
    {
        X = vector.x;
        Y = vector.y;
        Z = vector.z;
    }

    public Vector3 ToVector3() => new Vector3(X, Y, Z);

    // A default constructor is mandatory for `RealmObject`s but can be `private` if it is not to be used.
    private Vector3Entity()
    {
    }
}
