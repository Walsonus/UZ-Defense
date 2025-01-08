using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goldholder : MonoBehaviour
{
    public static int gold;
    public static int health = 5;
    public static bool isContinued = false;

    void Start()
    {
        gold = 100;
        health = 5;
    }

    public static void ContinueToTrue()
    {
        isContinued = true;
    }

    public static void ContinueToFalse()
    {
        isContinued = false;
    }

} 
