using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class SqliteExampleExtended : MonoBehaviour
{
    // Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/

    // 1
    [SerializeField] private int hitCountUnmodified = 0;
    [SerializeField] private int hitCountShift = 0;
    [SerializeField] private int hitCountControl = 0;

    // 2
    private const string HitCountKeyUnmodified = "HitCountKeyUnmodified";
    private const string HitCountKeyShift = "HitCountKeyShift";
    private const string HitCountKeyControl = "HitCountKeyControl";

    // 3
    private KeyCode modifier = default;

    void Start()
    {
        // Read all values from the table.
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM HitCountTableExtended";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read())
        {
            // The `id` has index 0, our `value` has the index 1.
            var id = dataReader.GetInt32(0);
            var value = dataReader.GetInt32(1);
            if (id == (int)KeyCode.LeftShift)
            {
                hitCountShift = value;
            }
            else if (id == (int)KeyCode.LeftControl)
            {
                hitCountControl = value;
            }
            else
            {
                hitCountUnmodified = value;
            }
        }

        // Remember to always close the connection at the end.
        dbConnection.Close();
    }

    private void Update() // 6
    {
        // Check if a key was pressed.
        if (Input.GetKey(KeyCode.LeftShift)) // 7
        {
            // Set the LeftShift key.
            modifier = KeyCode.LeftShift; // 8
        }
        else if (Input.GetKey(KeyCode.LeftControl)) // 7
        {
            // Set the LeftControl key.
            modifier = KeyCode.LeftControl; // 8
        }
        else // 9
        {
            // In any other case reset to default and consider it unmodified.
            modifier = default; // 10
        }
    }

    private void OnMouseDown()
    {
        var hitCount = 0;
        switch (modifier)
        {
            case KeyCode.LeftShift: // 11
                // Increment the hit count and set it to PlayerPrefs.
                hitCount = ++hitCountShift; // 12
                break;
            case KeyCode.LeftControl: // 11
                // Increment the hit count and set it to PlayerPrefs.
                hitCount = ++hitCountControl;
                break;
            default: // 13
                // Increment the hit count and set it to PlayerPrefs.
                hitCount = ++hitCountUnmodified;
                break;
        }

        // Insert a value into the table.
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand();
        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO HitCountTableExtended (id, value) VALUES (" + (int)modifier + ", " + hitCount + ")";
        dbCommandInsertValue.ExecuteNonQuery();
    }

    private IDbConnection CreateAndOpenDatabase()
    {
        // Open a connection to the database.
        string dbUri = "URI=file:MyDatabase.sqlite";
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand();
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTableExtended (id INTEGER PRIMARY KEY, value INTEGER)";
        dbCommandCreateTable.ExecuteReader();

        return dbConnection;
    }
}