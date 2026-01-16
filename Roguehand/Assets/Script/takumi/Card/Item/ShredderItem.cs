using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShredderItem : ItemBase
{
    int maxBreackCount = 2;
    public override void Initializ()
    {

    }

    public override void Use()
    {
        for(int i = 0; i < maxBreackCount; i++) 
        {
            List<int> indexs = RoleManager.instance.GetIndex();
            if (indexs.Count < 1) return;

            Card.Trump trump = CardManager.instance.GetPick()[0];

            CardObjectUtility.RemoveTrump(trump);

            indexs.RemoveAt(0);

            RoleManager.instance.SetIndex(indexs);

        }

        RoleManager.Role role = RoleManager.instance.RoleCheck(CardManager.instance.GetPick());

        RoleManager.instance.SetRole(role);

    }

}
