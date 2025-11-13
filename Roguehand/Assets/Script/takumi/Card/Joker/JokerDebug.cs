using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerDebug : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) JokerUtility.Addjoker(7/*(int)Random.Range(0, (int)ALLJoker._allJokerEnum.MAX)*//*((int)ALLJoker._allJokerEnum.MAX)-1*/);
        //if (Input.GetKeyDown(KeyCode.S)) JokerUtility.RoundStartJoker();
        //if (Input.GetKeyDown(KeyCode.M)) JokerUtility.JokerPlayStart();
        if (Input.GetKeyDown(KeyCode.A)) SaleObjectManager.instance.CreateRondom();
        if (Input.GetKeyDown(KeyCode.I)) ItemUtility.AddItem(0);
        //if (Input.GetKeyDown(KeyCode.Alpha1)) JokerObjectUtility.CardAddAction(1,2);
        //if (Input.GetKeyDown(KeyCode.Alpha0)) JokerUtility.SetTraget(JokerActionUseEnum.JokerActionTarget.item);

    }
}
