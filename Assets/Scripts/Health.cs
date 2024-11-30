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
     [SerializeField] private Color damageColor = Color.red;
     private Color originalColor;

    private SpriteRenderer sr;

    private bool isDestroyed = false;

     void Start()
    {
        // Initialize SpriteRenderer reference
        sr = GetComponent<SpriteRenderer>();
        
        // Ensure the SpriteRenderer is found
        if (sr != null)
        {
            // Store the original color of the sprite
            originalColor = sr.color;
        }
        else
        {
            Debug.LogWarning("SpriteRenderer not found on this GameObject.");
        }
    }

    //damage calculator
    public void DMG(int dmg){
        //reducing health points by invoked damage
        hp -= dmg;
        // Start the blink effect (flash red)
        StartCoroutine(FlashDamageEffect());
        //increasing coins for any hit
        BuildingManager.main.IncreaseCoins(coinsPerHit);
        //killing an enemy
        if(hp <= 0 && !isDestroyed){
            Spawner.onEnemyDestroy.Invoke();
            isDestroyed = true;
            Destroy(gameObject);
        }
    }


    // Coroutine to make the sprite flash red for a short duration
    private IEnumerator FlashDamageEffect()
    {
        // Change color to red
        sr.color = damageColor;

        // Wait for 0.1 seconds
        yield return new WaitForSeconds(0.1f);

        // Restore the original color
        sr.color = originalColor;
    }

}
