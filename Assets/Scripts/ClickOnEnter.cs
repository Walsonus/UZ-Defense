using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnEnter : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler

{
    [SerializeField] private AudioSource buttonaudioSource;
    [SerializeField] private AudioSource choiceaudioSource;
    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
{
    if (buttonaudioSource != null)
    {
        buttonaudioSource.Play();
    }
    else
    {
        Debug.LogError("buttonaudioSource is not assigned in the inspector!");
    }
}

public void OnPointerClick(PointerEventData eventData)
{
    if (choiceaudioSource != null)
    {
        choiceaudioSource.Play();
    }
    else
    {
        Debug.LogError("choiceaudioSource is not assigned in the inspector!");
    }
}

}
