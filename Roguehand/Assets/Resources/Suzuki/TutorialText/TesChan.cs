using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class TesChan : MonoBehaviour
{
    [NonSerialized]
    public string tutorial1;
    public List<string> tutorialText;
    int id = 20000;
    int index = 0;

    private void Start()
    {

        tutorial1 = MasterData.instance.GetStringMaster(20000);
        while(true)
        {
            // ‹ó‚ÌƒJƒ‰ƒ€‚ªŒ©‚Â‚©‚é‚Ü‚Å‚Ü‚í‚·
            if(MasterData.instance.GetStringMaster(id+index)=="") break;
            tutorialText.Add(MasterData.instance.GetStringMaster(id+index));
            index++;
        }

    }

}
