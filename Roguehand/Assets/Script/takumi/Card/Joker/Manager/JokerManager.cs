using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerManager : MonoBehaviour
{
    /// <summary>
    /// ジョーカーをまとめたリスト
    /// </summary>
    private List<JokerBase> _jokers;

    /// <summary>
    /// ジョーカーのターゲットになり得る物をキャッシュする
    /// </summary>
    private JokerActionUseEnum.JokerActionTarget _target;




    public void Awake()
    {
        
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

        _jokers.Remove(joker);
        return flag;
    }
    /// <summary>
    /// ジョーカーを追加する関数
    /// </summary>
    /// <param name="ID"></param>
    public void AddJoker(int ID) 
    {

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
