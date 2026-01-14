using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase
{

    private const int BOSS_NAME_RATE = 500;

    public int bossTextID = 0;

    public System.Action endAction = null;
    public virtual void Initializ() 
    {
        TextUIManager.instance.SetRoundNameText(GetBossName());

        TextUIManager.instance.SetRoundExceptionText(GetBossException());

    }

    public virtual void Update() { }
    public virtual void LateUpdate() { }


    public virtual void End() { endAction(); }

    public void SetAction(System.Action action) {endAction = action;}

    protected string GetBossName() { return  MasterData.instance.GetStringMaster(BOSS_NAME_RATE+ bossTextID); }
    protected string GetBossException() { return  MasterData.instance.GetStringMaster(bossTextID); }

    
}
