using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System;

public class Turret : MonoBehaviour
{

    [Header("References")]
    //point of rotation of a turret's weapon
    [SerializeField] private Transform rotationPoint;
    //project layer with enemies
    [SerializeField] private LayerMask enemyMask;
    //reference to a bullet object
    [SerializeField] private GameObject bulletRef;
    //spawn point for bullets on the turret
    [SerializeField] private Transform bulletPoint;
    //reference to the upgrade UI
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private Button upgradeButton;
    
    [Header("Attribute")]
    //targetting range of the turret
    [SerializeField] private float range = 4f;
    //rotation speed of a turret
    [SerializeField] private float rotationSpeed = 100f;
    //attack speed of the turret in shoots per second
    [SerializeField] private float fireRate = 2f;
    [SerializeField] private int baseCost = 100;

    private float baseRange;
    private float baseFireRate;
    

    //test function showing the range of the turret
        private void OnDrawGizmosSelected(){
            Handles.color = Color.blue;
            Handles.DrawWireDisc(transform.position, transform.forward, range);
        }

    //position of the turret target
    private Transform target;

    private float timeToNextBullet;
    private int turretLevel = 1;


    // Start is called before the first frame update
    void Start()
    {
        baseFireRate = fireRate;
        baseRange = range;

        upgradeButton.onClick.AddListener(UpgradeTurret);
        
    }

    // Update is called once per frame
    void Update()
    {

        if(target == null){
            FindTarget();
            return;
        }
        
        RotateToTarget();

        //if the target goes out of range, change target
        if(!IsTargetInRange()){
            target = null;

        //if the target is in range, shoot
        } else{
            
            timeToNextBullet += Time.deltaTime;

            //check if it is time to shoot
            if(timeToNextBullet >= 1f/fireRate){
                Shoot();
                timeToNextBullet = 0f;
            }
            
        }

    }

    //returns true if the distance between the target and the turret is smaller or equal turret range
    private bool IsTargetInRange(){

        if (Vector2.Distance(transform.position, target.position) <= range) return true;
        else return false; 
    }

    //function for finding a target
    private void FindTarget(){

        //array of rays confirming a "hit" on an enemy
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, range, (Vector2) transform.position, 0f, enemyMask);

        if(hits.Length > 0){
            //position of the first enemy found
            target = hits[0].transform;
        }
    }

    private void RotateToTarget(){
        //calculate the angle in radians between the current position and the target's position
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        //create a Quaternion rotation based on the calculated angle, rotating around the Z-axis
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));

        //applying the rotation to the turret (deltaTime added to remove FPS count affect on rotation speed)
        rotationPoint.rotation = Quaternion.RotateTowards(rotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //rotationPoint.rotation = targetRotation;
    }

    private void Shoot(){
        //getting the bullet object
                GameObject bulletInstance = Instantiate(bulletRef, bulletPoint.position, Quaternion.identity);
                //getting the bullet script inside bullet object
                Bullet bulletScr = bulletInstance.GetComponent<Bullet>();
                //setting the target in the .setTarget() function inside Bullet object
                bulletScr.SetTarget(target);
    }

    public void OpenUpgradeUI(){
        upgradeUI.SetActive(true);
    }

    public void closeUpgradeUI(){
        upgradeUI.SetActive(false);
        UIManager.main.SetHoveringState(false);
    }

    public void UpgradeTurret(){
        if(CostCalculator() <= BuildingManager.main.coins){
            BuildingManager.main.SpendCoins(CostCalculator());
            turretLevel += 1;

            fireRate = FireRateCalculator();
            range = RangeCalculator();

            closeUpgradeUI();
        }
    }

    private float FireRateCalculator(){
        return baseFireRate * Mathf.Pow(turretLevel, 0.5f);
    }

    private int CostCalculator(){
        return (int)Math.Round((double)(baseCost*2 * (turretLevel/2)));
    }

    private float RangeCalculator(){
        return baseRange * Mathf.Pow(turretLevel, 0.4f);
    }



}
