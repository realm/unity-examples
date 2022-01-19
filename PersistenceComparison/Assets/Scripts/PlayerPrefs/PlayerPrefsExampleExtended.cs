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
    private readonly string HitCountKeyUnmodified = "HitCountKeyUnmodified";
    private readonly string HitCountKeyShift = "HitCountKeyShift";
    private readonly string HitCountKeyControl = "HitCountKeyControl";

    private void Start()
    {
        // Check if the key exists. If not, we never saved the hit count before.
        if (PlayerPrefs.HasKey(HitCountKeyUnmodified)) // 3
        {
            // Read the hit count from the PlayerPrefs.
            hitCountUnmodified = PlayerPrefs.GetInt(HitCountKeyUnmodified); // 4
        }
        if (PlayerPrefs.HasKey(HitCountKeyShift)) // 3
        {
            // Read the hit count from the PlayerPrefs.
            hitCountShift = PlayerPrefs.GetInt(HitCountKeyShift); // 4
        }
        if (PlayerPrefs.HasKey(HitCountKeyControl)) // 3
        {
            // Read the hit count from the PlayerPrefs.
            hitCountControl = PlayerPrefs.GetInt(HitCountKeyControl); // 4
        }
    }

    private void OnMouseDown()
    {
        // Check if a key was pressed.
        if (Input.GetKey(KeyCode.LeftShift)) // 5
        {
            // Increment the hit count and set it to PlayerPrefs.
            hitCountShift++; // 6
            PlayerPrefs.SetInt(HitCountKeyShift, hitCountShift); // 9
        }
        else if (Input.GetKey(KeyCode.LeftControl)) // 5
        {
            // Increment the hit count and set it to PlayerPrefs.
            hitCountControl++; // 
            PlayerPrefs.SetInt(HitCountKeyControl, hitCountControl); // 9
        }
        else // 7
        {
            // Increment the hit count and set it to PlayerPrefs.
            hitCountUnmodified++; // 8
            PlayerPrefs.SetInt(HitCountKeyUnmodified, hitCountUnmodified); // 9
        }

        // Persist the data to disk.
        PlayerPrefs.Save(); // 10
    }

}
