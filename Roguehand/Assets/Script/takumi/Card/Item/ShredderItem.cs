using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class ShredderItem : ItemBase
{
    int maxBreackCount = 2;
    public override void Initializ()
    {

    }

    public override void Use()
    {
        List<Card.Trump> indexs = CardManager.instance.GetPick();
        for (int i = 0; i < maxBreackCount; i++)
        {

            if (indexs.Count < 1) return;
            CardObjectUtility.RemoveTrump(indexs[i]);

            indexs.RemoveAt(0);

        }

        RoleManager.Role role = RoleManager.instance.RoleCheck(CardManager.instance.GetPick());

        RoleManager.instance.SetRole(role);

    }

}
