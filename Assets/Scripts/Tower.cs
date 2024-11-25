using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Tower
{
    public String name;
    public int cost;
    public GameObject prefab;

    public Tower(string _name, int _cost, GameObject _prefab){
        name = _name;
        cost = _cost;
        prefab = _prefab;
    }
}
