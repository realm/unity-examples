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
    private readonly string hitCountKey = "HitCountKeyJson";

    private void Start()
    {
        // 4
        // Check if the key exists. If not, we never saved to it.
        if (PlayerPrefs.HasKey(hitCountKey))
        {
            // 5
            string jsonString = PlayerPrefs.GetString(hitCountKey);
            HitCount hitCount = JsonUtility.FromJson<HitCount>(jsonString);

            // 6
            if (hitCount != null)
            {
                hitCountUnmodified = hitCount.Unmodified;
                hitCountShift = hitCount.Shift;
                hitCountControl = hitCount.Control;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetKey(KeyCode.LeftShift)) // 7
        {
            // Increment the hit count and set it to PlayerPrefs.
            hitCountShift++; // 8
        }
        else if (Input.GetKey(KeyCode.LeftControl)) // 7
        {
            // Increment the hit count and set it to PlayerPrefs.
            hitCountControl++; // 8
        }
        else // 9
        {
            // Increment the hit count and set it to PlayerPrefs.
            hitCountUnmodified++; // 10
        }

        // 11
        HitCount hitCount = new();
        hitCount.Unmodified = hitCountUnmodified;
        hitCount.Shift = hitCountShift;
        hitCount.Control = hitCountControl;

        // 12
        string jsonString = JsonUtility.ToJson(hitCount);
        PlayerPrefs.SetString(hitCountKey, jsonString);
        PlayerPrefs.Save();
    }
}
