using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TextUIManager;

public class RunUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Initialize()
    {
        instance.Initialize();
        UIUtility.instance.Initialize();
        instance.SetRoundNameText("DebugBlind");
        instance.SetLowestScoreText("300");
        instance.SetRewardCountText(UIUtility.instance.RewardConversion(5));

    }

    
}
