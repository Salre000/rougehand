using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class AlternativeBoss : BossBase
{

    List<int> alternativeIndexs = new List<int>();


    private readonly int alternativeCount = 16;

    public override void Initializ()
    {
        base.Initializ();

        for (int i = 0; i < alternativeCount; i++)
        {
            int random = Random.Range(0, CardManager.instance.GetDeck().Count);

            if (alternativeIndexs.Exists(j => random == j)) { i--; continue; }

            alternativeIndexs.Add(random);

        }




    }

    public override void Update()
    {

    }

    public override void LateUpdate()
    {
        List<GameObject> alternativeObjects = new List<GameObject>();

        for (int i = 0; i < alternativeIndexs.Count; i++)
        {
            alternativeObjects.Add(CardObjectUtility.CardObjects()[alternativeIndexs[i]].gameObject);
        }

        for (int i = 0; i < alternativeObjects.Count; i++)
        {
            Vector3 vecter = alternativeObjects[i].transform.eulerAngles;
            vecter.y = 180;
            alternativeObjects[i].transform.eulerAngles = vecter;
        }

        for (int i = 0; i < alternativeObjects.Count; i++)
        {
            CardObject cardObject= alternativeObjects[i].GetComponent<CardObject>();

            if (cardObject.GetStatus() != CardObject.status.playWait) continue;

            TextUIManager.instance.SetRoleText("????");


        }


    }

    public override void End()
    {
        base.End();
    }









}
