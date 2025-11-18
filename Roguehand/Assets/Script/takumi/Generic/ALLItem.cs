using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ALLItem 
{
    public enum ALLItemEnum 
    {
        _constellation,
        _MAX,
    }

    public static ItemBase GetItem(ALLItemEnum item)
    {
        ItemBase itemBase = null;

        switch (item)
        {
            case ALLItemEnum._constellation: itemBase = new ConstellationItem(); break;

        }

        int itemID = (int)item;
        // 星座カードの分だけIDを前に進める
        if (itemID != 0) itemID += (int)ConstellationItem.ConstellationType.MAX;

        itemBase.SetItemID((int)item);

        return itemBase;
    }


}