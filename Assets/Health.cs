using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    
    [Header("Attributes")]
    //health points of an enemy
    [SerializeField] private int hp = 3;
    //damage calculator
    public void DMG(int dmg){
        //reducing health points by invoked damage
        hp -= dmg;

        //killing an enemy
        if(hp <= 0){
            Spawner.onEnemyDestroy.Invoke();
            Destroy(gameObject);
        }
    }

}
