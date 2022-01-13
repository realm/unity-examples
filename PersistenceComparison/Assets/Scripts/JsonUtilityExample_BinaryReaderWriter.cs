using System.IO;
using UnityEngine;

public class JsonUtilityExample_BinaryReaderWriter : MonoBehaviour
{
    // Resources:
    // https://docs.unity3d.com/ScriptReference/JsonUtility.html
    // https://docs.unity3d.com/Manual/script-Serialization.html
    // https://docs.unity3d.com/ScriptReference/Serializable.html

    public class HitCountWrapper
    {
        public int value;
    }

    [SerializeField] private int hitCount = 0;

    private readonly string fileName = "Temp/JsonUtilityExample_BinaryReaderWriter.bin";

    private void Start()
    {
        // Check if the file exists to avoid errors when opening a non-existing file.
        if (File.Exists(fileName))
        {
            FileStream fileStream = File.Open(fileName, FileMode.Open);
            string jsonString;
            using (BinaryReader binaryReader = new(fileStream))
            {
                jsonString = binaryReader.ReadString();
            }
            // Always close a FileStream when you're done with it.
            fileStream.Close();
            HitCountWrapper hitCountWrapper = JsonUtility.FromJson<HitCountWrapper>(jsonString);
            if (hitCountWrapper != null)
            {
                hitCount = hitCountWrapper.value;
            }
        }
    }

    private void OnMouseDown()
    {
        hitCount++;

        HitCountWrapper hitCountWrapper = new();
        hitCountWrapper.value = hitCount;
        string jsonString = JsonUtility.ToJson(hitCountWrapper);
        FileStream fileStream = File.Open(fileName, FileMode.Create);
        using (BinaryWriter binaryWriter = new(fileStream))
        {
            binaryWriter.Write(jsonString);
        }
        // Always close a FileStream when you're done with it.
        fileStream.Close();
    }
}
