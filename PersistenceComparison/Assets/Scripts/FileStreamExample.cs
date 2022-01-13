using UnityEngine;
using System;
using System.IO;
using System.Text;

public class FileStreamExample : MonoBehaviour
{
    // Resources:
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.file?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.filestream?view=net-5.0
    // https://docs.microsoft.com/en-us/dotnet/api/system.io.streamwriter?view=net-5.0

    [SerializeField] private int hitCountFileWriteAllText = 0;
    [SerializeField] private int hitCountFileWriteAllLines = 0;
    [SerializeField] private int hitCountFileStream = 0;
    [SerializeField] private int hitCountStreamWriter = 0;
    [SerializeField] private int hitCountStreamWriterFileStream = 0;

    private readonly string fileFileWriteAllText = "Temp/fileFileWriteAllText.txt";
    private readonly string fileFileWriteAllLines = "Temp/fileFileWriteAllLines.txt";
    private readonly string fileFileStream = "Temp/fileFileStream.txt";
    private readonly string fileStreamWriter = "Temp/fileStreamWriter.txt";
    private readonly string fileStreamWriterFileStream = "Temp/fileStreamWriterFileStream.txt";

    private void Start()
    {
        if (File.Exists(fileFileWriteAllText))
        {
            string textFileWriteAllText = File.ReadAllText(fileFileWriteAllText);
            hitCountFileWriteAllText = Int32.Parse(textFileWriteAllText);
        }

        if (File.Exists(fileFileWriteAllLines))
        {
            string[] textFileWriteAllLines = File.ReadAllLines(fileFileWriteAllLines);
            hitCountFileWriteAllLines = Int32.Parse(textFileWriteAllLines[0]);
        }

        if (File.Exists(fileFileStream))
        {
            using FileStream fileStream = File.OpenRead(fileFileStream);
            byte[] byteArray = new byte[1024];
            UTF8Encoding utf8Encoding = new(true);
            while (fileStream.Read(byteArray, 0, byteArray.Length) > 0)
            {
                hitCountFileStream = Int32.Parse(utf8Encoding.GetString(byteArray));
            }
        }

        if (File.Exists(fileStreamWriter))
        {
            using StreamReader streamReaderFile = new(fileStreamWriter);
            string textStreamReader = streamReaderFile.ReadLine();
            hitCountStreamWriter = Int32.Parse(textStreamReader);
        }

        if (File.Exists(fileStreamWriterFileStream))
        {
            using StreamReader streamReaderFileStream = new(new FileStream(fileStreamWriterFileStream, FileMode.Open));
            string textStreamReader2 = streamReaderFileStream.ReadLine();
            hitCountStreamWriterFileStream = Int32.Parse(textStreamReader2);
        }
    }

    private void OnMouseDown()
    {
        hitCountFileWriteAllText++;
        hitCountFileWriteAllLines++;
        hitCountFileStream++;
        hitCountStreamWriter++;
        hitCountStreamWriterFileStream++;

        // The easiest way when working with Files is to use them directly.
        // This writes all input at once and overwrites a file if executed again.
        // The File is opened and closed right away.
        File.WriteAllText(fileFileWriteAllText, hitCountFileWriteAllText.ToString());

        // You can also read and write line by line using a string array.
        // This also opens and closes the file right away and overwrites the
        // content.
        string[] stringArray = { hitCountFileWriteAllLines.ToString() };
        File.WriteAllLines(fileFileWriteAllLines, stringArray);

        // Instead of using a `File` you can use a `FileStream`.
        // Unlike `File` the `FileStream` can write multiple times.
        // Streams are kept open until closed.
        using FileStream fileStream = File.Create(fileFileStream);
        byte[] byteArray = new UTF8Encoding(true).GetBytes(hitCountFileStream.ToString());
        fileStream.Write(byteArray, 0, byteArray.Length);

        // The alternative to a `FileStream` would be a `StreamWriter`.
        // It can also write multiple times.
        // Streams are kept open until closed.
        using StreamWriter streamWriterFile = new(fileStreamWriter);
        streamWriterFile.Write(hitCountStreamWriter.ToString());

        // Finally, you can also use them together. The main advantage here is that passing a `FileStream`
        // into the `StreamWriter` instead of just a `File` is that it is more configurable.
        // Once again, it can write multiple times. Streams are kept open until closed.
        using StreamWriter straemWriterFileStream = new(new FileStream(fileStreamWriterFileStream, FileMode.Create));
        straemWriterFileStream.Write(hitCountStreamWriterFileStream.ToString());
    }

}
