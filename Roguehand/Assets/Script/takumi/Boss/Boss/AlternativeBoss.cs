using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeBoss : BossBase
{

    List<int> alternativeIndexs=new List<int>();

    List<GameObject> alternativeObjects=new List<GameObject>();

    private readonly int alternativeCount = 16;

    public override void Initializ()
    {
        for(int i=0;i< alternativeCount; i++) 
        {
            int random=Random.Range(0, CardManager.instance.GetDeck().Count);

            if (alternativeIndexs.Exists(j => random == j)) { i--; continue; }

            alternativeIndexs.Add(random);

        }

        for(int i=0;i< alternativeIndexs.Count; i++) 
        {
            alternativeObjects.Add(CardObjectUtility.CardObjects()[alternativeIndexs[i]].gameObject);
        }



    }

    public override void Update()
    {

    }

    public override void LateUpdate()
    {
        for (int i = 0; i < alternativeObjects.Count; i++)
            alternativeObjects[i].transform.eulerAngles = new Vector3(180, 0, 0);
    }

    public override void End()
    {
        base.End();
    }









}
