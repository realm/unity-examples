using UnityEngine;

public class PlayerPrefsExampleSimple : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/PlayerPrefs.html

    [SerializeField] private int hitCount = 0;

    private const string HitCountKey = "HitCountKey"; // 1

    private void Start()
    {
        // Check if the key exists. If not, we never saved the hit count before.
        if (PlayerPrefs.HasKey(HitCountKey)) // 2
        {
            // Read the hit count from the PlayerPrefs.
            hitCount = PlayerPrefs.GetInt(HitCountKey); // 3
        }
    }

    private void OnMouseDown()
    {
        hitCount++;

        // Set and save the hit count before ending the game.
        PlayerPrefs.SetInt(HitCountKey, hitCount); // 4
        PlayerPrefs.Save(); // 5
    }

}
