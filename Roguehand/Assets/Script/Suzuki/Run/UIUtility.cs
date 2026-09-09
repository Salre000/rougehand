using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using TMPro;
using UnityEngine;
using static TextUIManager;

public class UIUtility:MonoBehaviour
{
    public static UIUtility instance;

    private void Awake()
    {
        if (instance == null)
            instance=this;
    }

    public void Initialize()
    {
    }

    /// <summary>
    /// 報酬金の$を引数分追加
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
            dollStringBuilder.Append('$');
        }

        return dollStringBuilder.ToString();
    }
    /// <summary>
    /// 文字列を整数に変換、成功したならoutに出力
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public int IntTryParse(string value)
    {
        int result = 0;
        if (int.TryParse(value, out result))
        {
            return result;
        }
        Debug.Log("!!!FAILED!!!");
        return result = -1;
    }
}

