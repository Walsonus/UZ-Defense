using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager main;

    [Header("References")]
    [SerializeField] public List<Tower> towers = new List<Tower>();
    [SerializeField] private TextMeshProUGUI[] towerCost;

    public int coins;
    private int selectedTower = 0;
    private List<Plot> plots;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {

        plots = FindObjectsOfType<Plot>().ToList();
        
        if (Goldholder.gold < 100) Goldholder.gold = 100;

        coins = Goldholder.gold;
        setText();
        //SaveLoadManager.DeleteSaveData();
        if(Goldholder.isContinued == true){
            SaveData loadedData = SaveLoadManager.LoadGame();
            if (loadedData != null)
            {
                LoadGame(loadedData);
            }
        }
    }

    private void LoadGame(SaveData data)
    {

        
        Goldholder.gold = data.gold;
        coins = data.gold;
        Spawner.currentLevel = data.level + 1;
        //baseNumber += 2;
        Spawner.baseNumber = (Spawner.currentLevel*2)-1;
        //totalWaves+=1;
        Spawner.totalWaves = Spawner.currentLevel+1;

        foreach (SaveData.TurretData turretData in data.turrets)
        {
            GameObject prefab = GetTurretPrefabFromIdentifier(turretData.turretIdentifier);
            if (prefab != null)
            {
                Vector3 position = new Vector3(turretData.x, turretData.y, 0);
                Instantiate(prefab, position, Quaternion.identity);
                Debug.Log("Loaded turret with identifier: " + turretData.turretIdentifier);
            }
            else 
            {
                Debug.Log("Failed to load turret with identifier: " + turretData.turretIdentifier);
            }
        }

        foreach (SaveData.BuildingPlaceData plotData in data.buildingPlaces)
        {
            foreach (Plot plot in plots)
            {
                if (Mathf.Approximately(plot.transform.position.x, plotData.x) &&
                    Mathf.Approximately(plot.transform.position.y, plotData.y))
                {
                    plot.SetOccupied(plotData.isOccupied);
                    break;
                }
            }
        }
    }

    public void SaveGame()
    {
        SaveLoadManager.SaveGame(Spawner.currentLevel, coins, new List<Turret>(FindObjectsOfType<Turret>()), plots);
    }

    public void IncreaseCoins(int amount)
    {
        coins += amount;
    }

    public bool SpendCoins(int amount)
    {
        if (amount <= coins)
        {
            coins -= amount;
            return true;
        }
        else
        {
            Debug.Log("You don't have enough coins!");
            return false;
        }
    }

    public Tower GetSelectedTower()
    {
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower)
    {
        selectedTower = _selectedTower;
    }

    public void setText()
    {
        for (int i = 0; i < towerCost.Length; i++)
        {
            towerCost[i].text = towers[i].cost.ToString();
        }
    }

    public GameObject GetTurretPrefab(Tower tower)
    {
        return tower.prefab;
    }

    public GameObject GetTurretPrefabFromIdentifier(string identifier)
    {
        foreach (Tower tower in towers)
        {
            Debug.Log("Checking tower identifier: " + tower.name);
            if (tower.name == identifier)
            {
                Debug.Log("Match found for identifier: " + identifier);
                return tower.prefab;
            }
        }
        Debug.LogWarning("No match found for identifier: " + identifier);
        return null;
    }
}
