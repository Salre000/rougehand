using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class BossBase
{

    private const int BOSS_NAME_RATE = 500;

    public int bossTextID = 0;

    public System.Action endAction = null;

    public virtual void Initializ() 
    {
        BaseInitializ();
    }

    public void BaseInitializ() 
    {
        TextUIManager.instance.SetRoundNameText(GetBossName());

        TextUIManager.instance.SetRoundExceptionText(GetBossException());

    }

    public virtual void Update() { }
    public virtual void LateUpdate() { }


    public virtual void End() 
    {

        TextUIManager.instance.SetRoundNameText(string.Empty);

        TextUIManager.instance.SetRoundExceptionText(string.Empty);


        endAction();
   
    }

    public void SetAction(System.Action action) {endAction = action;}

    protected string GetBossName() { return  MasterData.instance.GetStringMaster(BOSS_NAME_RATE+ bossTextID); }
    protected string GetBossException() { return  MasterData.instance.GetStringMaster(bossTextID); }

    
}
