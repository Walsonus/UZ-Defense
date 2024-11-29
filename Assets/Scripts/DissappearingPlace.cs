using UnityEngine;

public class ChangeSpriteOnClick : MonoBehaviour
{
    public Sprite newSprite; // Sprite, na który chcesz zmienić
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        // Pobranie SpriteRenderer obiektu
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnMouseDown()
    {
        // Zmiana sprite'a przy kliknięciu
        if (newSprite != null)
        {
            spriteRenderer.sprite = null;
        }
    }
}