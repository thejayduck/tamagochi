using UnityEngine;
using Newtonsoft.Json;

public class SaveManager : MonoBehaviour
{
    private PetStats stats;
    private WardrobeManager wardrobe;
    private RoomManager room;

    private void Awake()
    {
        stats = PetStats.Instance;
        wardrobe = WardrobeManager.Instance;
        room = RoomManager.Instance;

        SaveSystem.Initialize();
    }
    
    private void OnGUI()
    {
         if (GUI.Button(new Rect(10, 10, 50, 50), "Save"))
         {
            Debug.Log("Saving");
            Save();
         }
         if (GUI.Button(new Rect(10, 100, 50, 50), "Load"))
         {
            Debug.Log("Loading");
            Load();
         } 
    }

    void Save()
    {
        float _experience = stats.TotalExperience;
        int _level = stats.CurrentLevel;

        float _affection = stats.Affection.Value;
        float _hunger = stats.Hunger.Value;
        float _cleanliness = stats.Cleanliness.Value;

        int _hatIndex = wardrobe.Hats.Index;
        int _glassesIndex = wardrobe.Glasses.Index;
        int _dressIndex = wardrobe.Dress.Index;
        int _accessoryIndex = wardrobe.Accessories.Index;

        int _roomIndex = room.CurrentRoom;

        SaveObject saveObject = new SaveObject {
            Experience = _experience,
            Level = _level,

            Affection = _affection,
            Hunger = _hunger,
            Cleanliness = _cleanliness,

            HatIndex = _hatIndex,
            GlassesIndex = _glassesIndex,
            DressIndex = _dressIndex,
            AccessoryIndex = _accessoryIndex,

            RoomIndex = _roomIndex,
        };
        string json = JsonConvert.SerializeObject(saveObject);
        SaveSystem.Save(json);
    }

    void Load()
    {
        string result = SaveSystem.Load();
        Debug.Log(result);

        if(result != null) {
            SaveObject saveObject = JsonConvert.DeserializeObject<SaveObject>(result);
            // stats.Initialize(saveObject);
        }
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