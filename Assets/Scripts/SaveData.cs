using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;
using TMPro;
[System.Serializable]
public class SaveData
{
    public int level;
    public int gold;
    public List<TurretData> turrets;
    public List<BuildingPlaceData> buildingPlaces;

    [System.Serializable]
    public class TurretData
    {
        public float x;
        public float y;
        public string turretIdentifier;
    }

    [System.Serializable]
    public class BuildingPlaceData
    {
        public float x;
        public float y;
        public bool isOccupied;
    }
}

