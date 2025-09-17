using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObjectManagerDebug : MonoBehaviour
{

    Card.Trump trump1=new Card.Trump();
    // Start is called before the first frame update
    void Start()
    {

        List<Card.Trump> ss = new List<Card.Trump>();

        Card.Trump trump = new Card.Trump();

        for (int i = 0; i < 8; i++) ss.Add(trump);

        CardObjectUtility.HandToCard(ss);

        CardObjectUtility.StartHandMove();
        
    }


    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.P)) CardObjectUtility.Play();
        if (Input.GetKeyDown(KeyCode.D)) CardObjectUtility.Discard();
        if (Input.GetKeyDown(KeyCode.E)) CardObjectUtility.End();
        if (Input.GetKeyDown(KeyCode.L)) CardObjectUtility.ChengeStandby(1);
        if (Input.GetKeyDown(KeyCode.C)) CardObjectUtility.SetChengeCard(1, trump1);
    }

}
