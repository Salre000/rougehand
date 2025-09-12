using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObjectManagerDebug : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

        List<Card.Trump> ss = new List<Card.Trump>();

        Card.Trump trump = new Card.Trump();

        for (int i = 0; i < 8; i++) ss.Add(trump);

        CardObjectUtility.HandToCard(ss);

        CardObjectUtility.StartHandMove();
    }

}
