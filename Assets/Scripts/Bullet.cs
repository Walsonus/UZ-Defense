using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    //position of the target
    private Transform target;

    //target will be set in the Shoot() script of a turret
    public void SetTarget(Transform enemy){
        target = enemy;
    }

    [Header("Attributes")]
    [SerializeField] private float bulletSpeed = 2f;
    [SerializeField] private int bulletDmg = 1;
    //slow effect
    [SerializeField] private bool slowsEnemy = false;


    [Header("References")]
    //RigidBody used to add velocity to the bullet
    [SerializeField] private Rigidbody2D rig;
    

    // Update is called once per frame
    private void FixedUpdate()
    {
        if(!target) return;
        Vector2 direction = (target.position - transform.position).normalized;
        rig.velocity = direction * bulletSpeed;
        // Rotate the bullet to always face the target
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90; // Convert the direction to an angle
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle)); // Apply the rotation to the bullet
    }

    void OnCollisionEnter2D(Collision2D col){
        //if (col.gameObject.CompareTag("Enemy"))
    //{
        //Handle enemy collision
        //Debug.Log("Collinded with "+col.gameObject.name);
        //reciveing the .DMG() function from the Health script

        if(slowsEnemy){
            col.gameObject.GetComponent<EnemyMovement>().UpdateSpeed(col.gameObject.GetComponent<EnemyMovement>().GetSpeed()/2);
        }

        col.gameObject.GetComponent<Health>().DMG(bulletDmg);
        Destroy(gameObject);
    //}
    }

    
}
