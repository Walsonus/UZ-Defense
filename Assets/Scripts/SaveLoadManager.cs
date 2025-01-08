using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveLoadManager
{
    private static string savePath = Application.persistentDataPath + "/savefile.dat";

    public static void SaveGame(int level, int gold, List<Turret> turretList, List<Plot> plotList)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Create(savePath))
            {
                SaveData data = new SaveData();
                data.level = level;
                data.gold = gold;
                data.turrets = new List<SaveData.TurretData>();
                data.buildingPlaces = new List<SaveData.BuildingPlaceData>();

                foreach (Turret turret in turretList)
                {
                    TurretIdentifier identifier = turret.GetComponent<TurretIdentifier>();
                    SaveData.TurretData turretData = new SaveData.TurretData
                    {
                        x = turret.transform.position.x,
                        y = turret.transform.position.y,
                        turretIdentifier = identifier.identifier
                    };
                    data.turrets.Add(turretData);
                }

                foreach (Plot plot in plotList)
                {
                    SaveData.BuildingPlaceData plotData = new SaveData.BuildingPlaceData
                    {
                        x = plot.transform.position.x,
                        y = plot.transform.position.y,
                        isOccupied = plot.isOccupied
                    };
                    data.buildingPlaces.Add(plotData);
                }

                bf.Serialize(file, data);
            }
        }
        catch (IOException e)
        {
            Debug.LogError("Save failed: " + e.Message);
        }
    }

    public static SaveData LoadGame()
    {
        if (File.Exists(savePath))
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                using (FileStream file = File.Open(savePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                {
                    SaveData data = (SaveData)bf.Deserialize(file);
                    return data;
                }
            }
            catch (IOException e)
            {
                Debug.LogError("Load failed: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }

        return null;
    }

    public static void DeleteSaveData()
    {
        if (File.Exists(savePath))
        {
            try
            {
                File.Delete(savePath);
                Debug.Log("Save file deleted.");
            }
            catch (IOException e)
            {
                Debug.LogError("Delete failed: " + e.Message);
            }
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }
    }
}
