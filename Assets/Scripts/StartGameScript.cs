using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScript : MonoBehaviour
{

    [SerializeField] private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener((value) => { Goldholder.gold = (int)value; });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
