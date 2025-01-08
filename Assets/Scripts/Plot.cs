using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plot : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private SpriteRenderer sr;
    [SerializeField] public Color hoverColor;
    private GameObject tower;
    public Turret turret;
    private Color startColor;
    public bool isOccupied = false; // New field to track occupancy

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        if (!isOccupied)
        {
            sr.color = hoverColor;
        }
    }

    private void OnMouseExit()
    {
        if (!isOccupied)
        {
            sr.color = startColor;
        }
    }

    private void OnMouseDown()
    {
        if (UIManager.main.IsHovering()) return;

        if (tower != null)
        {
            turret.OpenUpgradeUI();
            return;
        }

        Tower towerToBuild = BuildingManager.main.GetSelectedTower();
        if (towerToBuild.cost > BuildingManager.main.coins)
        {
            Debug.Log("Not enough money!");
            return;
        }

        BuildingManager.main.SpendCoins(towerToBuild.cost);

        tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity); // Create tower instance
        turret = tower.GetComponent<Turret>();
        sr.sprite = null;
        isOccupied = true; // Mark plot as occupied
    }
    
    // Method to manually set plot as occupied or not
    public void SetOccupied(bool occupied)
    {
        isOccupied = occupied;
        sr.sprite = occupied ? null : sr.sprite;
        gameObject.SetActive(!occupied);
    }
}
