using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandUpData : MonoBehaviour
{
    public static CommandUpData instance;

    private System.Action play;

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A)) Auto();

        if (Input.GetKeyDown(KeyCode.R)) ResultUIManager.Instance.Active();


    }

    private void Auto() 
    {

        // ボタン受付の停止中
        if (!GameUtility.IsPushButton()) return;
        if (GameUtility.IsPlay()) return;


        for (int i=0;i< CardManager.instance.GetPick().Count; i++) 
            CardManager.instance.SetIsSelect(CardManager.instance.GetHand().IndexOf(CardManager.instance.GetPick()[i]));
     
        RoleManager.instance.RoleCheck(CardManager.instance.GetHand());
        List<int> indexs =new List<int>( RoleManager.instance.GetIndex());
        for(int i = 0; i < indexs.Count; i++) 
        {
            CardManager.instance.SetIsSelect(indexs[i]);
        }
        play();

        // この方法だとスコアの加算を行わずに行ける
        //CardManager.instance.ResetPick();
        //RoleManager.instance.RoleCheck(CardManager.instance.GetPick());

    }

    public void SetPlay(System.Action action) {  play = action; }

    private void OnApplicationQuit()
    {
        // ここにセーブ処理を記述します。
        // 例えば、ゲームデータを保存するメソッド。
        MemoryManager.Keep();

    }
}
