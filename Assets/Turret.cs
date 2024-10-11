using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{

    [Header("References")]
    //point of rotation of a turret's weapon
    [SerializeField] private Transform rotationPoint;
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    //targetting range of the turret
    [SerializeField] private float range = 4f;

    //test function showing the range of the turret
        private void OnDrawGizmosSelected(){
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, transform.forward, range);
        }

    //position of the turret target
    private Transform target;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target == null){
            findTarget();
            return;
        }
        
        rotateToTarget();

    }

    //function for finding a target
    private void findTarget(){

        //array of rays confirming a "hit" on an enemy
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2) transform.position, 0f, enemyMask);

        if(hits.Length > 0){
            //position of the first enemy found
            target = hits[0].transform;
        }
    }

    private void rotateToTarget(){
        //calculate the angle in radians between the current position and the target's position
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        //create a Quaternion rotation based on the calculated angle, rotating around the Z-axis
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        //applying the rotation to the turret
        rotationPoint.rotation = targetRotation;
    }

}
