using System.IO;
using UnityEngine;

public class BinaryReaderWriterExample : MonoBehaviour
{
    // Resources:
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.binarywriter?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.binaryreader?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.stream?view=net-5.0

    [SerializeField] private int hitCount = 0;

    private readonly string fileName = "Temp/BinaryReaderWriterExample"; // 1

    private void Start()
    {
        // Check if the file exists to avoid errors when opening a non-existing file.
        if (File.Exists(fileName)) // 2
        {
            // Open a stream to the file that the `BinaryReader` can use to read data.
            // They need to be disposed at the end, so `using` is good practice
            // because it does this automatically.
            using FileStream fileStream = File.Open(fileName, FileMode.Open); // 3
            using BinaryReader binaryReader = new(fileStream); // 4
            hitCount = binaryReader.ReadInt32(); // Exception if type is not correct.
        }
    }

    private void OnMouseDown()
    {
        hitCount++;

        // Open a stream to the file that the `BinaryReader` can use to read data.
        // They need to be disposed at the end, so `using` is good practice
        // because it does this automatically.
        using FileStream fileStream = File.Open(fileName, FileMode.Create); // 5
        using BinaryWriter binaryWriter = new(fileStream); // 6
        binaryWriter.Write(hitCount); // 7
    }

}
