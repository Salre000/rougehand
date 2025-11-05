using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFPS : MonoBehaviour
{
    public void Awake()
    {
        Application.targetFrameRate = 120;
    }
}
