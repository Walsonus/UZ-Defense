using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plot : MonoBehaviour
{
   [Header("References")]
   [SerializeField] private SpriteRenderer sr;
   [SerializeField] private Color hoverColor;
    private GameObject tower;
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
        if(tower != null) return;

        Tower towerToBuild = BuildingManager.main.GetSelectedTower();
        if (towerToBuild.cost > BuildingManager.main.coins){
            Debug.Log("Not enough money!");
            return;
        } 

        BuildingManager.main.SpendCoins(towerToBuild.cost);

    tower = Instantiate(towerToBuild.prefab, transform.position, Quaternion.identity); // Twórz instancję wieży
}

}
