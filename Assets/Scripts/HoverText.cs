using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;  // Potrzebne do obsługi interakcji z myszką
using UnityEngine.UI;
using Unity.VisualScripting;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textMeshPro; // Referencja do TMP
    [SerializeField] private Color hoverColor; // Kolor podświetlenia
    private Color originalColor; // Oryginalny kolor tekstu
    private static HoverText currentlySelected;
    private string originalText;
    [SerializeField] private string hoverText;

    private void Start()
    {
    if (textMeshPro != null)
        {
            originalText = string.IsNullOrEmpty(originalText) ? textMeshPro.text : originalText; // Zachowaj bieżący tekst jako domyślny
            originalColor = textMeshPro.color; // Zachowaj bieżący kolor jako domyślny
        }
    }
    // Obsługuje najechanie myszką na tekst
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (textMeshPro != null)
        {
            // Jeśli hoverText jest ustawiony, zmień tekst
            if (!string.IsNullOrEmpty(hoverText))
            {
                textMeshPro.text = hoverText;
            }

            // Zmień kolor na podświetlony
            textMeshPro.color = hoverColor;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (textMeshPro != null)
        {
            // Jeśli originalText został ustawiony, przywróć go
            if (!string.IsNullOrEmpty(originalText))
            {
                textMeshPro.text = originalText;
            }

            // Przywróć oryginalny kolor
            textMeshPro.color = originalColor;
        }
    }
}