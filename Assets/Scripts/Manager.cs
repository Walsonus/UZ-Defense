using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public static Manager main;
    
    public Transform startPoint;
    public Transform[] path;
    private void Awake()
    {
        main = this;
    }
}
