using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;  // Potrzebne do obsługi interakcji z myszką

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textMeshPro; // Referencja do TMP
    [SerializeField] private Color hoverColor = Color.yellow; // Kolor podświetlenia
    private Color originalColor; // Oryginalny kolor tekstu

    private void Start()
    {
        // Pobierz oryginalny kolor tekstu
        originalColor = textMeshPro.color;
    }

    // Obsługuje najechanie myszką na tekst
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Zmieniamy kolor tekstu na podświetlony
        textMeshPro.color = hoverColor;
    }

    // Obsługuje opuszczenie obszaru tekstu
    public void OnPointerExit(PointerEventData eventData)
    {
        // Przywracamy oryginalny kolor
        textMeshPro.color = originalColor;
    }
}