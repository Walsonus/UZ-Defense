using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    // Singleton instance of UIManager
    public static UIManager main;

    // Flag to track hover state
    private bool hovered;

    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Set the singleton instance
        main = this;
    }

    // Sets the hover state
    public void SetHoveringState(bool state)
    {
        hovered = state;
    }

    // Returns whether the UI is being hovered over
    public bool IsHovering()
    {
        return hovered;
    }
}
