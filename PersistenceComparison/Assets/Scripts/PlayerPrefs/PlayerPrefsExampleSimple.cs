using UnityEngine;

public class PlayerPrefsExampleSimple : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/PlayerPrefs.html

    [SerializeField] private int hitCount = 0;

    private readonly string hitCountKey = "HitCountKey"; // 1

    private void Start()
    {
        // Check if the key exists. If not, we never saved the hit count before.
        if (PlayerPrefs.HasKey(hitCountKey)) // 2
        {
            // Read the hit count from the PlayerPrefs.
            hitCount = PlayerPrefs.GetInt(hitCountKey); // 3
        }
    }

    private void OnMouseDown()
    {
        hitCount++;

        // Set and save the hit count before ending the game.
        PlayerPrefs.SetInt(hitCountKey, hitCount); // 4
        PlayerPrefs.Save(); // 5
    }

}
