using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string SAVE_LOC => Application.dataPath + "/Save/main.sav";

    private PetStats stats;
    private WardrobeManager wardrobe;
    private RoomManager room;

    private void Awake()
    {
        stats = PetStats.Instance;
        wardrobe = WardrobeManager.Instance;
        room = RoomManager.Instance;
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 50, 50), "Save"))
        {
            Debug.Log("Saving..");
            Save();
        }
        if (GUI.Button(new Rect(10, 100, 50, 50), "Load"))
        {
            Debug.Log("Loading..");
            Load();
        }
    }

    struct SaveFile
    {
        public string IV;
        public string Data;
    }

    void Save()
    {
        SaveObject saveObject = new SaveObject
        {
            Experience = stats.TotalExperience,
            Level = stats.CurrentLevel,

            Affection = stats.Affection.Value,
            Hunger = stats.Hunger.Value,
            Cleanliness = stats.Cleanliness.Value,

            HatIndex = wardrobe.Hats.Index,
            GlassesIndex = wardrobe.Glasses.Index,
            DressIndex = wardrobe.Dress.Index,
            AccessoryIndex = wardrobe.Accessories.Index,

            RoomIndex = room.CurrentRoom,
        };

        if (!Directory.Exists(SAVE_LOC))
        {
            Directory.CreateDirectory(Directory.GetParent(SAVE_LOC).ToString());
        }

        string json = JsonConvert.SerializeObject(saveObject);

        string iv;
        string data;

        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String("UJ3JMqwz+uD/nIVQDbDhtLHj39E77Am6X3yd9pRKjFQ=");
            aes.GenerateIV();
            iv = Convert.ToBase64String(aes.IV);
            var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream();
            using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            using (var sw = new StreamWriter(cs))
            {
                sw.Write(json);
            }
            data = Convert.ToBase64String(ms.ToArray());
        }

        using (var fs = File.OpenWrite(SAVE_LOC))
        using (var sw = new StreamWriter(fs))
        using (var js = new JsonTextWriter(sw))
        {
            var ser = new JsonSerializer();
            ser.Serialize(js, new SaveFile()
            {
                IV = iv,
                Data = data
            });
        }
    }

    void Load()
    {
        SaveFile save;
        using (var fs = File.OpenRead(SAVE_LOC))
        using (var sw = new StreamReader(fs))
        using (var js = new JsonTextReader(sw))
        {
            var ser = new JsonSerializer();
            save = ser.Deserialize<SaveFile>(js);
        }

        var data = Convert.FromBase64String(save.Data);
        string json;

        using (var aes = Aes.Create())
        {
            aes.Key = Convert.FromBase64String("UJ3JMqwz+uD/nIVQDbDhtLHj39E77Am6X3yd9pRKjFQ=");
            aes.IV = Convert.FromBase64String(save.IV);
            var encryptor = aes.CreateDecryptor(aes.Key, aes.IV);

            using var ms = new MemoryStream(data);
            using CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Read);
            using var sw = new StreamReader(cs);
            json = sw.ReadToEnd();
        }

        var saveData = JsonConvert.DeserializeObject<SaveObject>(json);

        stats.TotalExperience = saveData.Experience;
        stats.CurrentLevel = saveData.Level;

        stats.Affection.Value = saveData.Affection;
        stats.Hunger.Value = saveData.Hunger;
        stats.Cleanliness.Value = saveData.Cleanliness;

        wardrobe.Set(wardrobe.Hats, saveData.HatIndex);
        wardrobe.Set(wardrobe.Glasses, saveData.GlassesIndex);
        wardrobe.Set(wardrobe.Dress, saveData.DressIndex);
        wardrobe.Set(wardrobe.Accessories, saveData.AccessoryIndex);

        room.SwitchRoom(saveData.RoomIndex);
    }
}

public struct SaveObject
{
    // Progress
    public float Experience;
    public int Level;

    // Stats
    public float Affection;
    public float Hunger;
    public float Cleanliness;

    // Clothing
    public int HatIndex;
    public int GlassesIndex;
    public int DressIndex;
    public int AccessoryIndex;

    // Other
    public int RoomIndex;
}