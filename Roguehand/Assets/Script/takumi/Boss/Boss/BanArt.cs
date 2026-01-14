using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class BanArt : BossBase
{

    bool OneFlag = false;

    Card.suit suit;

    System.Action endAction;


    public override void Initializ()
    {

        suit = (Card.suit)Random.Range(0, (int)Card.suit.max);

        TextUIManager.instance.SetRoundNameText(GetBossName());

        TextUIManager.instance.SetRoundExceptionText(GetBossException()+MasterData.instance.GetStringMaster((int)suit)+")");





    }

    public override void Update()
    {



    }

    public override void LateUpdate()
    {
        bool flag = false;
        CardObjectUtility.GetCardHands().GetAction(hand => { if (hand.GetStatus() == CardObject.status.play) flag = true; return hand; });

        if (!flag) return;

        Debug.Log("’Ê‚Á‚Ä‚¢‚é");

        List<int> indexs = RoleManager.instance.GetIndex();

        for (int i=0;i< CardManager.instance.GetPick().Count; i++) 
        {
            if (CardManager.instance.GetPick()[i].suit != suit) continue;

            if (indexs.FindIndex(index=>index==i)<0) continue;


            indexs.Remove(i);

        }

        RoleManager.instance.SetIndex(indexs);


    }

    public override void End()
    {
        base.End();
    }










}
