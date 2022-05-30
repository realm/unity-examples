using UnityEngine;
using Realms;

public class RealmExampleExtended : MonoBehaviour
{
    // Resources:
    // https://github.com/realm/realm-dotnet

    [SerializeField] private int hitCountUnmodified = 0;
    [SerializeField] private int hitCountShift = 0;
    [SerializeField] private int hitCountControl = 0;

    private KeyCode modifier = default;
    private Realm realm;
    private HitCountExtended hitCount;

    void Start()
    {
        // Open a database connection.
        realm = Realm.GetInstance();

        hitCount = realm.Find<HitCountExtended>(1);
        if (hitCount != null)
        {
            // Read the hit count data from the database.
            hitCountUnmodified = hitCount.Unmodified;
            hitCountShift = hitCount.Shift;
            hitCountControl = hitCount.Control;
        }
        else
        {
            // In case the database was empty, create a new `hitCount`.
            hitCount = new HitCountExtended(1);
            realm.Write(() =>
            {
                realm.Add(hitCount);
            });
        }
    }

    private void Update()
    {
        // Check if a key was pressed.
        if (Input.GetKey(KeyCode.LeftShift))
        {
            // Set the LeftShift key.
            modifier = KeyCode.LeftShift;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            // Set the LeftControl key.
            modifier = KeyCode.LeftControl;
        }
        else
        {
            // In any other case reset to default and consider it unmodified.
            modifier = default;
        }
    }

    private void OnMouseDown()
    {
        switch (modifier)
        {
            case KeyCode.LeftShift:
                hitCountShift++;
                break;
            case KeyCode.LeftControl:
                hitCountControl++;
                break;
            default:
                hitCountUnmodified++;
                break;
        }

        realm.Write(() =>
        {
            hitCount.Unmodified = hitCountUnmodified;
            hitCount.Shift = hitCountShift;
            hitCount.Control = hitCountControl;
        });
    }
}