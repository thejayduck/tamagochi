using System.IO;
using UnityEngine;

public static class SaveSystem
{
    private static readonly string SAVE_LOC = Application.dataPath + "/Save/";

    public static void Initialize()
    {
        if(!Directory.Exists(SAVE_LOC))
        {
            Directory.CreateDirectory(SAVE_LOC);
        }
    }

    public static void Save(string input)
    {
        File.WriteAllText(SAVE_LOC + "/save.json", input);
    }

    public static string Load()
    {
        if (File.Exists(SAVE_LOC + "/save.json")) {
            string result = File.ReadAllText(SAVE_LOC + "/save.json");
            return result;
        }
        else {
            return null;
        }
            
    }
    
}