using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerManager : MonoBehaviour
{
    /// <summary>
    /// ジョーカーをまとめたリスト
    /// </summary>
    private List<JokerBase> _jokers;

    public void Awake()
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



}
