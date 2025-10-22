using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ALLItem 
{
    public enum ALLItemEnum 
    {
        _constellation,
    }

    public static ItemBase GetItem(ALLItemEnum item)
    {
        ItemBase itemBase = null;

        switch (item)
        {
            case ALLItemEnum._constellation: itemBase = new ConstellationItem(); break;

        }

        itemBase.SetItemID((int)item);

        return itemBase;
    }


}