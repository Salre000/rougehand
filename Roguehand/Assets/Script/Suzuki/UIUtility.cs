using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using static UIManager;

public class UIUtility:MonoBehaviour
{
    private static UIUtility instance;

    public void Initialize()
    {



    }

    /// <summary>
    /// •ñV‹à‚Ì$‚ğˆø”•ª’Ç‰Á
    /// </summary>
    /// <param name="reward"></param>
    /// <returns></returns>
    public string RewardConversion(int reward)
    {
        StringBuilder dollStringBuilder = new StringBuilder();
        if(reward<0)
            reward = 0;
        for(int i = 0;i<reward;i++)
        {
            dollStringBuilder.Append("$");
        }

        return dollStringBuilder.ToString();
    }

    public static UIUtility getIns()
    {
        if (instance == null)
        {

            instance = GameObject.Find("UIManager").GetComponent<UIUtility>();
        }

        return (instance);
    }
}
