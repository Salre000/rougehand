using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakUtility
{

    public static BreakManager instance { private get; set; }


    public static void StartBreak(GameObject gameObject) { instance.StartBreak(gameObject); }


    

}