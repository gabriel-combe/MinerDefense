using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Amount of Dollars")]
    [SerializeField]
    private int dollars;

    [Header("Base Stats")]
    [SerializeField]
    private int goldBase = 7;
    [SerializeField]
    private int buildingLifeBase = 20;
    [SerializeField]
    private int turretDamageBase = 5;
    [SerializeField]
    private float turretAttackSpeedBase = 0.25f;

    [Header("Stats Increment")]
    [SerializeField]
    private int goldIncr = 1;
    [SerializeField]
    private int buildingLifeIncr = 5;
    [SerializeField]
    private int turretDamageIncr = 2;
    [SerializeField]
    private float turretAttackSpeedIncr = 0.1f;

    private static GameSaveData saveData = new GameSaveData();

    // Data to save
    [System.Serializable]
    public struct GameSaveData
    {
        public int dollars;
        public int startingGoldLevel;
        public int buildingLifeLevel;
        public int turretDamageLevel;
        public int turretAttackSpeedLevel;
    }

    // Keep the game manager between scenes
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if(objs.Length > 1)
            Destroy(gameObject);
    
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Load the save if it exists, otherwise set the save data to its base value
        if (File.Exists(SaveFileName())) {
            Load();
        } else {
            saveData.dollars = 0;
            saveData.startingGoldLevel = 0;
            saveData.buildingLifeLevel = 0;
            saveData.turretDamageLevel = 0;
            saveData.turretAttackSpeedLevel = 0;
            Save();
        }
    }

    public static string SaveFileName()
    {
        string saveFile = Application.persistentDataPath + "/save.save";
        return saveFile;
    }

    // Save into a json
    public static void Save()
    {
        File.WriteAllText(SaveFileName(), JsonUtility.ToJson(saveData, true));
    }

    // Load from a json
    public static void Load()
    {
        string saveContent = File.ReadAllText(SaveFileName());

        saveData = JsonUtility.FromJson<GameSaveData>(saveContent);
    }

    // Data getter
    public int GetDollars() { return  saveData.dollars; }
    public int GetStartingGoldLevel() { return saveData.startingGoldLevel; }
    public int GetBuildingLifeLevel() { return saveData.buildingLifeLevel; }
    public int GetTurretDamageLevel() { return saveData.turretDamageLevel; }
    public int GetTurretAttackSpeedLevel() { return saveData.turretAttackSpeedLevel; }
    public int GetStartingGold() { return goldBase + (saveData.startingGoldLevel * goldIncr); }
    public int GetBuildingLife() { return buildingLifeBase + (saveData.buildingLifeLevel * buildingLifeIncr); }
    public int GetTurretDamage() { return turretDamageBase + (saveData.turretDamageLevel * turretDamageIncr); }
    public float GetTurretAttackSpeed() { return turretAttackSpeedBase + (saveData.turretAttackSpeedLevel * turretAttackSpeedIncr); }


    // Data update
    public void AddDollars(int dollars) { saveData.dollars += dollars; }
    public void UpgradeStartingGold() { saveData.startingGoldLevel++; }
    public void UpgradeBuildingLife() { saveData.buildingLifeLevel++; }
    public void UpgradeTurretDamage() { saveData.turretDamageLevel++; }
    public void UpgradeTurretAttackSpeed() { saveData.turretAttackSpeedLevel++; }
    public bool RemoveDollars(int dollars) {
        if (saveData.dollars < dollars) return false;

        saveData.dollars -= dollars;

        return true;
    }

}

