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
        if (File.Exists(HitCountFileJson))
        {
            string jsonString = File.ReadAllText(HitCountFileJson);
            HitCount hitCount = JsonUtility.FromJson<HitCount>(jsonString);
            if (hitCount != null)
            {
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
        if (modifier == KeyCode.LeftShift)
        {
            // Increment the Shift hit count.
            hitCountShift++;
        }
        else if (modifier == KeyCode.LeftControl)
        {
            // Increment the Control hit count.
            hitCountControl++;
        }
        else
        {
            // If neither Shift nor Control was held, we increment the unmodified hit count.
            hitCountUnmodified++;
        }

        // Create a new HitCount object to hold this data.
        HitCount hitCount = new();
        hitCount.Unmodified = hitCountUnmodified;
        hitCount.Shift = hitCountShift;
        hitCount.Control = hitCountControl;

        // Create a JSON using the HitCount object.
        string jsonString = JsonUtility.ToJson(hitCount, true);

        // Save the json to the file.
        File.WriteAllText(HitCountFileJson, jsonString);
    }
}
