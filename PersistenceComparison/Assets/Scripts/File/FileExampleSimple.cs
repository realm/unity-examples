using System;
using System.IO;
using UnityEngine;

public class FileExampleSimple : MonoBehaviour
{
    // Resources:
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.file?view=net-5.0

    [SerializeField] private int hitCount = 0;

    private const string HitCountFile = "hitCountFile.txt";

    private void Start()
    {
        if (File.Exists(HitCountFile))
        {
            string textFileWriteAllText = File.ReadAllText(HitCountFile);
            hitCount = Int32.Parse(textFileWriteAllText);
        }
    }

    private void OnMouseDown()
    {
        hitCount++;

        // The easiest way when working with Files is to use them directly.
        // This writes all input at once and overwrites a file if executed again.
        // The File is opened and closed right away.
        File.WriteAllText(HitCountFile, hitCount.ToString());
    }

}
