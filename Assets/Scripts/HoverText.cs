using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;  // Potrzebne do obsługi interakcji z myszką
using UnityEngine.UI;
using Unity.VisualScripting;

public class HoverText : MonoBehaviour,IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI textMeshPro; // Referencja do TMP
    [SerializeField] private Color hoverColor = Color.yellow; // Kolor podświetlenia
    private Color originalColor; // Oryginalny kolor tekstu
    private bool isSelected = false;
    private static HoverText currentlySelected;

    private void Start()
    {
        // Pobierz oryginalny kolor tekstu
        originalColor = textMeshPro.color;
    }

    // Obsługuje najechanie myszką na tekst
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Zmieniamy kolor tekstu na podświetlony
        if(currentlySelected != this){
        textMeshPro.color = hoverColor;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Zmieniamy kolor tekstu na podświetlony
        if(currentlySelected != null && currentlySelected != this){
            currentlySelected.OnDeselect();
        }
        textMeshPro.color = hoverColor;
        currentlySelected = this;
    }

    // Obsługuje opuszczenie obszaru tekstu
    public void OnPointerExit(PointerEventData eventData)
    {
        // Przywracamy oryginalny kolor
        if(currentlySelected != this){
        textMeshPro.color = originalColor;
        }
    }

    public void OnDeselect(){
        textMeshPro.color = originalColor;
    }
}