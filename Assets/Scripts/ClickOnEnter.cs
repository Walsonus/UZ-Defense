using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickOnEnter : MonoBehaviour, IPointerEnterHandler

{
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    public void OnPointerEnter(PointerEventData eventData)
    {
        audioSource.Play();
    }
}
