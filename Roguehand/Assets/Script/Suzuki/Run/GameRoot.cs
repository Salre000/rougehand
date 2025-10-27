using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TextUIManager;

public class GameRoot:MonoBehaviour 
{
    [SerializeField] GameObject _dontTouchZone;
    [SerializeField] GameObject _shopCanvas;
    bool _next = false;
    private void Start()
    {
        _dontTouchZone.SetActive(false);
        _shopCanvas.SetActive(false);
    }
    private void Update()
    {
        if(_next) return;
        RoundClearCheck();
    }
    void RoundClearCheck()
    {
        return;
        int left=0, right=0;
        left = instance.IntTryParse(instance.GetLowestScoreText().text);
        right = instance.IntTryParse(instance.GetRoundScoreText().text);

        if(left <= right)
        {
            StartCoroutine(NextRound());
        }
    }

    IEnumerator NextRound()
    {
        _next = true;
        _dontTouchZone.SetActive(true);
        yield return new WaitForSeconds(1);
        //instance.InitializeText();
        _shopCanvas.SetActive(true);
    }

}
