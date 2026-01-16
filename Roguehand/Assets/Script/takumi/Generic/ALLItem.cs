using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ALLItem 
{
    public enum ALLItemEnum 
    {
        _constellation,
        _shredderItem,
        _MAX,
    }

    public static ItemBase GetItem(ALLItemEnum item)
    {
        ItemBase itemBase = null;

        switch (item)
        {
            case ALLItemEnum._constellation: itemBase = new ConstellationItem(); break;
            case ALLItemEnum._shredderItem:  itemBase = new ShredderItem(); break;
            case ALLItemEnum._MAX:
                break;
        }

        int itemID = (int)item;
        // 星座カードの分だけIDを前に進める
        if (itemID != 0) itemID += (int)ConstellationItem.ConstellationType.MAX-1;

        itemBase.SetItemID((int)itemID);

        return itemBase;
    }


}