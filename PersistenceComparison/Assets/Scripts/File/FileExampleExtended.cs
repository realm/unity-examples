using System;
using System.IO;
using UnityEngine;

public class FileExampleExtended : MonoBehaviour
{
    // Resources:
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.file?view=net-5.0

    [SerializeField] private int hitCountUnmodified = 0;
    [SerializeField] private int hitCountShift = 0;
    [SerializeField] private int hitCountControl = 0;

    private KeyCode modifier = default;

    private const string HitCountFileUnmodified = "hitCountFileExtended.txt";

    private void Start()
    {
        // Check if the file exists. If not, we never saved before.
        if (File.Exists(HitCountFileUnmodified))
        {
            // Read all lines.
            string[] textFileWriteAllLines = File.ReadAllLines(HitCountFileUnmodified);

            // For this extended example we would expect to find three lines, one per counter.
            if (textFileWriteAllLines.Length == 3)
            {
                // Set the counters correspdoning to the entries in the array.
                hitCountUnmodified = Int32.Parse(textFileWriteAllLines[0]);
                hitCountShift = Int32.Parse(textFileWriteAllLines[1]);
                hitCountControl = Int32.Parse(textFileWriteAllLines[2]);
            }
        }
    }

    private void Update() // 1
    {
        // Check if a key was pressed.
        if (Input.GetKey(KeyCode.LeftShift)) // 2
        {
            // Set the LeftShift key.
            modifier = KeyCode.LeftShift; // 3
        }
        else if (Input.GetKey(KeyCode.LeftControl)) // 2
        {
            // Set the LeftControl key.
            modifier = KeyCode.LeftControl; // 3
        }
        else // 4
        {
            // In any other case reset to default and consider it unmodified.
            modifier = default; // 5
        }
    }

    private void OnMouseDown() // 6
    {
        // Check if a key was pressed.
        switch (modifier)
        {
            case KeyCode.LeftShift: // 7
                // Increment the Shift hit count.
                hitCountShift++; // 8
                break;
            case KeyCode.LeftCommand: // 7
                // Increment the Control hit count.
                hitCountControl++; // 8
                break;
            default: // 9
                // If neither Shift nor Control was held, we increment the unmodified hit count.
                hitCountUnmodified++; // 10
                break;
        }

        // 11
        // Create a string array with the three hit counts.
        string[] stringArray = {
            hitCountUnmodified.ToString(),
            hitCountShift.ToString(),
            hitCountControl.ToString()
        };

        // 12
        // Save the entries, line by line.
        File.WriteAllLines(HitCountFileUnmodified, stringArray);
    }

}
