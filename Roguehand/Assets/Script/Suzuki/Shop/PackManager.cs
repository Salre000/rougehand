using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackManager : MonoBehaviour
{
    public static PackManager instance;

    private bool _isBuyPack=false;

    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void SetIsBuyPack(bool flag) {  _isBuyPack = flag; }
    public bool IsBuyPack() { return _isBuyPack; } 
}
