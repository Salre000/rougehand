using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        UIManager.Initialize();
        UIUtility.getIns().Initialize();
        UIManager.SetRoundNameText("DebugBlind");
        UIManager.SetLowestScoreText("300");
       UIManager.SetRewardCountText(UIUtility.getIns().RewardConversion(5));

    }

    
}
