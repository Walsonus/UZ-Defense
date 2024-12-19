using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [Header("References")]
    [SerializeField] TextMeshProUGUI coinsUI;
    [SerializeField] Animator anim;

    private bool isMenuOpen = false;

    public void ToggleMenu(){
        isMenuOpen = !isMenuOpen;
        anim.SetBool("MenuOpen", isMenuOpen);
    }

    private void OnGUI(){
        coinsUI.text = BuildingManager.main.coins.ToString();
    }

    public void SetSelected(){
        
    }
}
