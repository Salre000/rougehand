using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting;

public class JokerManager : MonoBehaviour
{
    /// <summary>
    /// ジョーカーをまとめたリスト
    /// </summary>
    private List<JokerBase> _jokers=new List<JokerBase>(5);

    /// <summary>
    /// ジョーカーのターゲットになり得る物をキャッシュする
    /// </summary>
    private JokerActionUseEnum.JokerActionTarget _target;



    public void Awake()
    {
        JokerUtility.instance = this;
    }

    private void Update()
    {
    }


    /// <summary>
    /// ジョーカーを破棄する関数
    /// </summary>
    /// <param name="joker"></param>
    /// <returns></returns>
    public bool Remove(JokerBase joker) 
    {

        bool flag=_jokers.Contains(joker);

        int index = _jokers.FindIndex(jokerBase => joker == jokerBase);

        _jokers.Remove(joker);

        
        //ジョーカーのオブジェクトの削除処理
        JokerObjectUtility.RemoveJoker(index);

        return flag;
    }

    /// <summary>
    /// ラウンドの開始時のジョーカーの処理
    /// </summary>
    public void RoundStart() 
    {
       for(int i=0;i< _jokers.Count;i++) _jokers[i].RoundStart();
    }
    /// <summary>
    /// ラウンドの終了時のジョーカーの処理
    /// </summary>
    public void RoundEnd() 
    {

       for(int i=0;i< _jokers.Count;i++) _jokers[i].RoundEnd();
    }


    /// <summary>
    /// ジョーカーを追加する関数
    /// </summary>
    /// <param name="ID"></param>
    public void AddJoker(int ID) 
    {
        JokerBase joker = ALLJoker.GetJoker((ALLJoker._allJokerEnum)ID);
        _jokers.Add(joker);
        JokerObjectUtility.AddJoker(joker);

    }

    /// <summary>
    /// 今のフレームないで行われたターゲットの動き
    /// </summary>
    /// <returns></returns>
    public JokerActionUseEnum.JokerActionTarget GetTarget() {return _target;}

    /// <summary>
    /// ジョーカーによって倍率が上昇する関数
    /// </summary>
    /// <param name="magnification"></param>
    public void JokerAddMagnification(float magnification) 
    {

    }
    /// <summary>
    /// ジョーカーによって基礎値が上昇する関数
    /// </summary>
    /// <param name="baseValue"></param>
    public void JokerAddBaseValue(float baseValue) 
    {

    }

}
