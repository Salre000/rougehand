using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreViewControle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // ラウンドスコアが加算されたタイミング
        if (!GameUtility.IsRoundScoreUp()) return;
        ClearScoreViewText();
    }

    // ビューテキストを空にする
    public void ClearScoreViewText()
    {
        ScoreManager.instance.SetViewIndex(0);
        for (int i = 0; i < ScoreManager.instance.SCORE_INDEX_MAX; i++)
            ScoreManager.instance.SetScoreViewText(0);
        ScoreManager.instance.SetViewIndex(0);
    }
}
