using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

public class GameRoot:MonoBehaviour 
{
    private void Update()
    {
        RoundClearCheck();
    }
    void RoundClearCheck()
    {
        int left=0, right=0;
        left = IntTryParse(GetLowestScoreText());
        right = IntTryParse(GetRoundScoreText());

        if(left <= right)
        {
            string num = "10000";
            SetLowestScoreText(num);
        }
    }

}
