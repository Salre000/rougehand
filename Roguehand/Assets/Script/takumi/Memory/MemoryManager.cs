using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryManager
{
    public static Memory instantMemory = null;

    public static void CreateMemory()
    {
        instantMemory = new Memory();
    }


    public static void Use()
    {
        if (instantMemory != null)
        {
            instantMemory.Use();
        }
        else
        {
            instantMemory = new Memory(string.Empty);
        }
    }



}
