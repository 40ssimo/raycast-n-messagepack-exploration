using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using MessagePack;

public class SaveLoadManager : MonoBehaviour
{
    private string saveFilePath;

    private void Start()
    {
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.bin");
    }

    public void SavePlayerData(PlayerData playerData)
    {
        var lz4Options = MessagePackSerializerOptions.Standard.WithCompression(MessagePackCompression.Lz4Block);

        byte[] bytes = MessagePackSerializer.Serialize(playerData, lz4Options);
        File.WriteAllBytes(saveFilePath, bytes);
        Debug.Log(playerData.bootsColor + playerData.glassesColor);
    }

    public PlayerData LoadPlayerData()
    {
        if (File.Exists(saveFilePath))
        {
            byte[] bytes = File.ReadAllBytes(saveFilePath);
            return MessagePackSerializer.Deserialize<PlayerData>(bytes);
            

        } else
        {
            var newPlayerData = new PlayerData { bootsColor = new Color(0f, 0f, 0f, 0f) , glassesColor = new Color(0f, 0f, 0f, 0f)};
            Debug.LogWarning("Save file not found");
            return newPlayerData;
        }
    }
}
