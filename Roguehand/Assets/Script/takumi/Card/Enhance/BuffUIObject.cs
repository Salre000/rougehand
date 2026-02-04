using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuffUIObject : MonoBehaviour
{
    private GameObject parent = null;
    [SerializeField] private GameObject uiObject;
    List<GameObject> buffUI = new List<GameObject>();

    //—LŒø‚É‚È‚Á‚½Žž
    private void OnEnable()
    {
        return;
        parent = transform.parent.gameObject;


        TypeJoker();

    }
    private void OnDisable()
    {

        return;
        for(int i=0;i< buffUI.Count;i++)
            Destroy(buffUI[i]);

        buffUI.Clear();
    }

    private void TypeJoker()
    {

        GameObject Related = ExplanationManager.instance.RelatedObject(parent);
        if (Related == null) return;

        JokerObject jokerObject = Related.GetComponent<JokerObject>();

        if (jokerObject == null) return;

        int index = JokerObjectUtility.GetJokerIndex(jokerObject);

        JokerUtility.JokerALLAction(joker =>
        {
            if (JokerUtility.GetIndex(joker) != index) return;


            if (joker.GetCardBuff() != Card.cardBuff.None)
            {
                buffUI.Add(CreateUI(-1,-1));
            }
            if (joker.GetJokerBuff() != Card.JokerBuff.None) 
            {

                buffUI.Add(CreateUI(-1,-1));
            }

            for(int i = 0; i < buffUI.Count; i++) 
            {
                buffUI[i].transform.parent = transform;





            }

        });




    }

    private GameObject CreateUI(int ID, int ID2)
    {
        GameObject game = GameObject.Instantiate(uiObject);

        game.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = MasterData.instance.GetStringMaster(ID);
        game.transform.GetChild(1).transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = MasterData.instance.GetStringMaster(ID2);
        return game;

    }




}
