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
        if (OneFlag) return;
        bool flag = false;
        CardObjectUtility.GetCardHands().GetAction(hand => { if (hand.GetStatus() == CardObject.status.play) flag = true; return hand; });

        if (!flag) return;

        List<Card.Trump> hands = CardManager.instance.GetHand();

        for(int i=0;i< CardObjectUtility.GetCardHands().Count; i++) 
        {
            //if (CardManager.instance.GetHand()[i].suit != suit) continue;

            //if (RoleManager.instance.GetIndex().Exists(j => i != j)) continue;

            //RoleManager.instance.GetIndex().GetAction(value => { if (i == value) return -1; return value; });


        }



    }

    public override void End()
    {
        base.End();
    }










}
