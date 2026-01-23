using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFPS : MonoBehaviour
{

    public static SetFPS setFPS;
    public void Awake()
    {
        if (setFPS != null) 
        {
            Destroy(this);
        }
        else 
        {
            DontDestroyOnLoad(this.gameObject);
            setFPS = this;
        }
        Application.targetFrameRate = 120;
    }
}
