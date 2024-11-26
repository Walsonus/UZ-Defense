using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UpgradeUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Flag to track if the mouse is over the UI element
    private bool mouseOver = false;
        
    // Called when the pointer enters the UI element
    public void OnPointerEnter(PointerEventData eventData)
    {
        mouseOver = true;
        // Set the hovering state in the UIManager
        UIManager.main.SetHoveringState(true);
    }

    // Called when the pointer exits the UI element
    public void OnPointerExit(PointerEventData eventData)
    {
        mouseOver = false;
        // Reset the hovering state in the UIManager
        UIManager.main.SetHoveringState(false);
        // Deactivate the UI element when the pointer exits
        gameObject.SetActive(false);
    }
}
