using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Delete : BossBase
{
    private int handSizeLost=6;
    int handSize = 0;
    public override void Initializ()
    {
        base.Initializ();
        handSize = CardManager.instance.GetHandSize();

        CardManager.instance.SetHandSize(handSizeLost);

    }

    public override void Update()
    {

    }

    public override void LateUpdate()
    {
    }

    public override void End()
    {
        base.End();
        CardManager.instance.SetHandSize(handSize);


    }
}
