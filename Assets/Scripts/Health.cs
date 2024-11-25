using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    [Header("Attributes")]
    //health points of an enemy
    [SerializeField] private int hp;

    //coins for hit an enemy
    [SerializeField] private int coinsPerHit;

    private bool isDestroyed = false;
    //damage calculator
    public void DMG(int dmg){
        //reducing health points by invoked damage
        hp -= dmg;
        //increasing coins for any hit
        BuildingManager.main.IncreaseCoins(coinsPerHit);
        //killing an enemy
        if(hp <= 0 && !isDestroyed){
            Spawner.onEnemyDestroy.Invoke();
            
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

}
