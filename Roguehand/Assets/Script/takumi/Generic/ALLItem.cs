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
        switch (item)
        {
            case ALLItemEnum._constellation: return new ConstellationItem();

        }
        return null;
    }


}