using Mono.Data.Sqlite;
using System.Data;
using UnityEngine;

public class SqliteExample : MonoBehaviour
{
    // Resources:
    // https://www.mono-project.com/docs/database-access/providers/sqlite/

    [SerializeField] private int hitCount = 0;

    void Start()
    {
        // Read all values from the table.
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandReadValues = dbConnection.CreateCommand();
        dbCommandReadValues.CommandText = "SELECT * FROM HitCountTable";
        IDataReader dataReader = dbCommandReadValues.ExecuteReader();

        while (dataReader.Read())
        {
            // The `id` has index 0, our `value` has the index 1.
            hitCount = dataReader.GetInt32(1);
        }

        // Remember to always close the connection at the end.
        dbConnection.Close();
    }

    private void OnMouseDown()
    {
        hitCount++;

        // Insert a value into the table.
        IDbConnection dbConnection = CreateAndOpenDatabase();
        IDbCommand dbCommandInsertValue = dbConnection.CreateCommand();
        dbCommandInsertValue.CommandText = "INSERT OR REPLACE INTO HitCountTable (id, value) VALUES (0, " + hitCount + ")";
        dbCommandInsertValue.ExecuteNonQuery();
    }

    private IDbConnection CreateAndOpenDatabase()
    {
        // Open a connection to the database.
        string dbUri = "URI=file:Temp/MyDatabase";
        IDbConnection dbConnection = new SqliteConnection(dbUri);
        dbConnection.Open();

        // Create a table for the hit count in the database if it does not exist yet.
        IDbCommand dbCommandCreateTable = dbConnection.CreateCommand();
        dbCommandCreateTable.CommandText = "CREATE TABLE IF NOT EXISTS HitCountTable (id INTEGER PRIMARY KEY, value INTEGER )";
        dbCommandCreateTable.ExecuteReader();

        return dbConnection;
    }
}