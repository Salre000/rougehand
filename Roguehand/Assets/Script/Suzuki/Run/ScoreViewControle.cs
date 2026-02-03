using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreViewControle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // ビューテキストを空白にする
        for(int i =0; i<ScoreManager.VIEW_INDEX_MAX; i++)
            ScoreManager.instance.SetScoreViewText("");
        ScoreManager.instance.SetViewIndex(0);
    }

    // Update is called once per frame
    void Update()
    {
        // ラウンドスコアが加算されたタイミング
        if (!GameUtility.IsRoundScoreUp()) return;
    }

    // ビューテキストを空にする
    public void ClearScoreViewText()
    {
        for (int i = 0; i < ScoreManager.VIEW_INDEX_MAX; i++)
            ScoreManager.instance.SetScoreViewText("");
        ScoreManager.instance.SetViewIndex(0);
    }
}
