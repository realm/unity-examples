using UnityEngine;
using Realms;

public class RealmExampleSimple : MonoBehaviour
{
    // Resources:
    // https://github.com/realm/realm-dotnet

    [SerializeField] private int hitCounter = 0;

    private Realm realm;
    private HitCount hitCount;

    void Start()
    {
        // Open a database connection.
        realm = Realm.GetInstance();

        hitCount = realm.Find<HitCount>(1);
        if (hitCount != null)
        {
            // Read the hit count data from the database.
            hitCounter = hitCount.Value;
        }
        else
        {
            // In case the database was empty, create a new `hitCount`.
            hitCount = new HitCount(1);
            realm.Write(() =>
            {
                realm.Add(hitCount);
            });
        }
    }

    private void OnMouseDown()
    {
        hitCounter++;

        realm.Write(() =>
        {
            hitCount.Value = hitCounter;
        });
    }

}