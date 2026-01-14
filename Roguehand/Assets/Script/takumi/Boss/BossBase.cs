using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBase
{
    protected int bossTextID = 0;

    public System.Action endAction = null;
    public virtual void Initializ() { }

    public virtual void Update() { }
    public virtual void LateUpdate() { }


    public virtual void End() { endAction(); }

    public void SetAction(System.Action action) {endAction = action;}

}
