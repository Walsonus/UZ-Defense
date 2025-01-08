using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class SliderHealthScrpit : MonoBehaviour
{

    [SerializeField] public Slider slider;
    [SerializeField] private TextMeshProUGUI valueText;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = Goldholder.health;
        valueText.text = slider.value.ToString();
        //update text to display current value
        slider.onValueChanged.AddListener((value) => {
            valueText.text = value.ToString();
            Goldholder.health = (int)value;
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
