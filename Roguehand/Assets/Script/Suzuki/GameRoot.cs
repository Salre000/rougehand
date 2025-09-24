using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UIManager;

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
        int left=0, right=0;
        left = IntTryParse(GetLowestScoreText());
        right = IntTryParse(GetRoundScoreText());

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
        UIManager.InitializeText();
        _shopCanvas.SetActive(true);
    }

}
