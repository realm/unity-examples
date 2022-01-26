using UnityEngine;

public class PlayerPrefsExampleJson : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/JsonUtility.html
    // https://docs.unity3d.com/Manual/script-Serialization.html
    // https://docs.unity3d.com/ScriptReference/Serializable.html
    // https://docs.unity3d.com/Manual/JSONSerialization.html

    // 1
    private class HitCount
    {
        public int Unmodified;
        public int Shift;
        public int Control;
    }

    // 2
    [SerializeField] private int hitCountUnmodified = 0;
    [SerializeField] private int hitCountShift = 0;
    [SerializeField] private int hitCountControl = 0;

    // 3
    private const string HitCountKey = "HitCountKeyJson";

    // 4
    private KeyCode modifier = default;

    private void Start()
    {
        // 5
        // Check if the key exists. If not, we never saved to it.
        if (PlayerPrefs.HasKey(HitCountKey))
        {
            // 6
            var jsonString = PlayerPrefs.GetString(HitCountKey);
            var hitCount = JsonUtility.FromJson<HitCount>(jsonString);

            // 7
            if (hitCount != null)
            {
                hitCountUnmodified = hitCount.Unmodified;
                hitCountShift = hitCount.Shift;
                hitCountControl = hitCount.Control;
            }
        }
    }

    private void Update() // 8
    {
        // Check if a key was pressed.
        if (Input.GetKey(KeyCode.LeftShift)) // 9
        {
            // Set the LeftShift key.
            modifier = KeyCode.LeftShift; // 10
        }
        else if (Input.GetKey(KeyCode.LeftControl)) // 9
        {
            // Set the LeftControl key.
            modifier = KeyCode.LeftControl; // 10
        }
        else // 11
        {
            // In any other case reset to default and consider it unmodified.
            modifier = default; // 12
        }
    }

    private void OnMouseDown()
    {
        // Check if a key was pressed.
        switch (modifier)
        {
            case KeyCode.LeftShift: // 13
                // Increment the hit count and set it to PlayerPrefs.
                hitCountShift++; // 14
                break;
            case KeyCode.LeftCommand: // 13
                // Increment the hit count and set it to PlayerPrefs.
                hitCountControl++; // 14
                break;
            default: // 15
                // Increment the hit count and set it to PlayerPrefs.
                hitCountUnmodified++; // 16
                break;
        }

        // 17
        HitCount hitCount = new();
        hitCount.Unmodified = hitCountUnmodified;
        hitCount.Shift = hitCountShift;
        hitCount.Control = hitCountControl;

        // 18
        var jsonString = JsonUtility.ToJson(hitCount);
        PlayerPrefs.SetString(HitCountKey, jsonString);
        PlayerPrefs.Save();
    }
}
