using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ƒpƒbƒNw“ü
/// </summary>
public class BuyPack:MonoBehaviour
{
    private void Update()
    {
        Buy();
    }

    private void Buy()
    {
        if (!PackManager.instance.IsBuyPack()) return;


    }

}
