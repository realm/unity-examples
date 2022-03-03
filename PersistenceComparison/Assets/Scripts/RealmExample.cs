using UnityEngine;
using Realms;

public class HitCountEntity : RealmObject
{
    [PrimaryKey]
    public int PrimaryKey { get; set; }
    public int Value { get; set; }

    private HitCountEntity() { }

    public HitCountEntity(int primaryKey)
    {
        PrimaryKey = primaryKey;
    }
}

public class RealmExample : MonoBehaviour
{
    // Resources:
    // https://github.com/realm/realm-dotnet

    [SerializeField] private int hitCount = 0;

    private Realm realm;
    private HitCountEntity hitCountEntity;

    private void Start()
    {
        // Open a database connection.
        realm = Realm.GetInstance();
        
        hitCountEntity = realm.Find<HitCountEntity>(1);
        if (hitCountEntity != null)
        {
            // Read the hit count data from the database.
            hitCount = hitCountEntity.Value;
        }
        else
        {
            // In case the database was empty, create a new `HitCountEntity`.
            hitCountEntity = new HitCountEntity(1);
            realm.Add(hitCountEntity);
        }
    }

    private void OnApplicationQuit()
    {
        

        realm.Dispose();
    }

    private void OnMouseDown()
    {
        hitCount++;

        realm.Write(() =>
        {
            hitCountEntity.Value = hitCount;
        });
    }
}
