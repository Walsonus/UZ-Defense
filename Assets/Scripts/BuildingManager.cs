using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public static BuildingManager main;

    [Header("References")]
    // [SerializeField] private GameObject[] towerPrefabs;
    [SerializeField] private Tower[] towers;

    public int coins = 100;
    private int selectedTower = 0;

    private void Awake(){
        main = this;
    }
    
    //coins for start
    private void Start(){
        
    }

    //increasing coins method
    public void IncreaseCoins(int amount){
        coins += amount;
    }

    //spending coins method
    public bool SpendCoins(int amount){
        if(amount <= coins){
            coins -= amount;
            return true;
        } else {
            Debug.Log("You don't have enough coins!");
            return false;
        }
    }

    public Tower GetSelectedTower(){
        return towers[selectedTower];
    }

    public void SetSelectedTower(int _selectedTower){
        selectedTower = _selectedTower;
    }
}
