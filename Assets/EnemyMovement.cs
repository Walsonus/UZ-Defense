using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    //rigid body reference
    [Header("References")]
    [SerializeField] private Rigidbody2D rigbody;

    //Attribute for move speed for enemies
    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 0.5f;

    private Transform target;
    //start of path by path index
    private int pathId = 0;

    private void Start()
    {
        //start point for moving enemy
        target = Manager.main.path[pathId];
    }
    private void Update()
    {
        //Vector2 is euclides distance by two points in 2D {distance by target position and actual target distance} 
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            //if the condition becomes true - pathId'll be incremented, and target start point will be for example second
            pathId++;

            //verify that length is equal of path distance
            if (pathId == Manager.main.path.Length)
            {
                //if condition was reached - target'll be destroyed
                Destroy(gameObject);
                return;
            }
            else
            {
                target = Manager.main.path[pathId];
            }
        }
    }
    //FixedUpdate is special method that is performed in regular periods
    private void FixedUpdate()
    {
        //normalized is changing direction vector in unit vector
        Vector2 direction = (target.position - transform.position).normalized;
        rigbody.velocity = direction * moveSpeed;
    }
}
