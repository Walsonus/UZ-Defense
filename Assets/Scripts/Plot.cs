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
    public  Turret turret;
    private Color startColor;

    private void Start(){
        startColor = sr.color;
    }
    private void OnMouseEnter(){
        sr.color = hoverColor;
    }

    private void OnMouseExit(){
        sr.color = startColor;
    }

    private void OnMouseDown(){        
        if(UIManager.main.IsHovering()) return;

        if(tower != null) {
            turret.OpenUpgradeUI();
            return;
            }

        Tower towerToBuild = BuildingManager.main.GetSelectedTower();
        if (towerToBuild.cost > BuildingManager.main.coins){
            Debug.Log("Not enough money!");
            return;
        } 

        BuildingManager.main.SpendCoins(towerToBuild.cost);

    tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity); // Twórz instancję wieży
    turret = tower.GetComponent<Turret>();
    sr.sprite = null;
}

}
