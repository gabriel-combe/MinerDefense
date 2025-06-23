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
    private int buildingLifeBase = 50;
    [SerializeField]
    private int turretDamageBase = 10;
    [SerializeField]
    private float turretAttackSpeedBase = 1f;

    [Header("Stats Increment")]
    [SerializeField]
    private int goldIncr = 1;
    [SerializeField]
    private int buildingLifeIncr = 5;
    [SerializeField]
    private int turretDamageIncr = 2;
    [SerializeField]
    private float turretAttackSpeedIncr = 0.4f;

    [Header("Stats upgrade")]
    [SerializeField]
    private static GameSaveData saveData = new GameSaveData();

    // Data to save
    [System.Serializable]
    public struct GameSaveData
    {
        public int dollars;
        public int startingGold;
        public int buildingLife;
        public int turretDamage;
        public float turretAttackSpeed;
    }

    // Keep the game manager between scenes
    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameManager");

        if(objs.Length > 1)
            Destroy(this.gameObject);
    
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        // Load the save if it exists, otherwise set the save data to its base value
        if (File.Exists(SaveFileName())) {
            Load();
        } else {
            saveData.dollars = 0;
            saveData.startingGold = goldBase;
            saveData.buildingLife = buildingLifeBase;
            saveData.turretDamage = turretDamageBase;
            saveData.turretAttackSpeed = turretAttackSpeedBase;
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
    public int GetStartingGold() { return saveData.startingGold; }
    public int GetBuildingLife() { return saveData.buildingLife; }
    public int GetTurretDamage() { return saveData.turretDamage; }
    public float GetTurretAttackSpeed() { return saveData.turretAttackSpeed; }


    // Data update
    public void AddDollars(int dollars) { saveData.dollars = dollars; }
    public void UpgradeStartingGold() { saveData.startingGold += goldIncr; }
    public void UpgradeBuildingLife() { saveData.buildingLife += buildingLifeIncr; }
    public void UpgradeTurretDamage() { saveData.turretDamage += turretDamageIncr; }
    public void UpgradeTurretAttackSpeed() { saveData.turretAttackSpeed += turretAttackSpeedIncr; }
    public bool RemoveDollars(int dollars) {
        if (saveData.dollars < dollars) return false;

        saveData.dollars -= dollars;

        return true;
    }

}

