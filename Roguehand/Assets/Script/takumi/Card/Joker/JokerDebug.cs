using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) JokerUtility.Addjoker(2);
        if (Input.GetKeyDown(KeyCode.S)) JokerUtility.RoundStartJoker();
        if (Input.GetKeyDown(KeyCode.M)) JokerUtility.JokerPlayStart();
        if (Input.GetKeyDown(KeyCode.C)) JokerUtility.ChengeOrder(0,1);
        if (Input.GetKeyDown(KeyCode.Alpha1)) JokerObjectUtility.CardAddAction(1,2);
        if (Input.GetKeyDown(KeyCode.Alpha0)) JokerUtility.SetTraget(JokerActionUseEnum.JokerActionTarget.item);



    }
}
