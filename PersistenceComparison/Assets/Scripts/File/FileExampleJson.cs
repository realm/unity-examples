using System.IO;
using UnityEngine;

public class FileExampleJson : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/JsonUtility.html
    // https://docs.unity3d.com/Manual/script-Serialization.html
    // https://docs.unity3d.com/ScriptReference/Serializable.html

    private class HitCount
    {
        public int Unmodified;
        public int Shift;
        public int Control;
    }

    [SerializeField] private int hitCountUnmodified = 0;
    [SerializeField] private int hitCountShift = 0;
    [SerializeField] private int hitCountControl = 0;

    private KeyCode modifier = default;

    private const string HitCountFileJson = "hitCountFileJson.txt";

    private void Start()
    {
        // Check if the file exists to avoid errors when opening a non-existing file.
        if (File.Exists(HitCountFileJson)) // 5
        {
            // 6
            var jsonString = File.ReadAllText(HitCountFileJson);
            var hitCount = JsonUtility.FromJson<HitCount>(jsonString);

            // 7
            if (hitCount != null)
            {
                // 8
                hitCountUnmodified = hitCount.Unmodified;
                hitCountShift = hitCount.Shift;
                hitCountControl = hitCount.Control;
            }
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
        // 1
        // Check if a key was pressed.
        switch (modifier)
        {
            case KeyCode.LeftShift:
                // Increment the Shift hit count.
                hitCountShift++;
                break;
            case KeyCode.LeftCommand:
                // Increment the Control hit count.
                hitCountControl++;
                break;
            default:
                // If neither Shift nor Control was held, we increment the unmodified hit count.
                hitCountUnmodified++;
                break;
        }

        // 2
        // Create a new HitCount object to hold this data.
        var updatedCount = new HitCount
        {
            Unmodified = hitCountUnmodified,
            Shift = hitCountShift,
            Control = hitCountControl,
        };

        // 3
        // Create a JSON using the HitCount object.
        var jsonString = JsonUtility.ToJson(updatedCount, true);

        // 4
        // Save the json to the file.
        File.WriteAllText(HitCountFileJson, jsonString);
    }
}
