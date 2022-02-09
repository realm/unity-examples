using UnityEngine;

public class PlayerPrefsExampleExtended : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/PlayerPrefs.html

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

    private void Start()
    {
        // Check if the key exists. If not, we never saved the hit count before.
        if (PlayerPrefs.HasKey(HitCountKeyUnmodified)) // 4
        {
            // Read the hit count from the PlayerPrefs.
            hitCountUnmodified = PlayerPrefs.GetInt(HitCountKeyUnmodified); // 5
        }
        if (PlayerPrefs.HasKey(HitCountKeyShift)) // 4
        {
            // Read the hit count from the PlayerPrefs.
            hitCountShift = PlayerPrefs.GetInt(HitCountKeyShift); // 5
        }
        if (PlayerPrefs.HasKey(HitCountKeyControl)) // 4
        {
            // Read the hit count from the PlayerPrefs.
            hitCountControl = PlayerPrefs.GetInt(HitCountKeyControl); // 5
        }
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
        // Check if a key was pressed.
        switch (modifier)
        {
            case KeyCode.LeftShift: // 11
                // Increment the hit count and set it to PlayerPrefs.
                hitCountShift++; // 12
                PlayerPrefs.SetInt(HitCountKeyShift, hitCountShift); // 15
                break;
            case KeyCode.LeftControl: // 11
                // Increment the hit count and set it to PlayerPrefs.
                hitCountControl++; // 
                PlayerPrefs.SetInt(HitCountKeyControl, hitCountControl); // 15
                break;
            default: // 13
                // Increment the hit count and set it to PlayerPrefs.
                hitCountUnmodified++; // 14
                PlayerPrefs.SetInt(HitCountKeyUnmodified, hitCountUnmodified); // 15
                break;
        }

        // Persist the data to disk.
        PlayerPrefs.Save(); // 16
    }

}
