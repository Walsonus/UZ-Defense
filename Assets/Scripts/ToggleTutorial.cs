using UnityEngine;

public class ToggleTutorial : MonoBehaviour
{
    public GameObject panel; // Panel do otwierania/zamykania

    public void TogglePanel()
    {
        if (panel != null)
        {
            panel.SetActive(!panel.activeSelf); // Przełączanie widoczności
        }
    }
}
