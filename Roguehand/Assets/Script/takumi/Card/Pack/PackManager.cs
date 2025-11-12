using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackManager : MonoBehaviour
{
    public static PackManager instance;

    [SerializeField] private Transform _leftPos;
    [SerializeField] private Transform _rightPos;


    private readonly int MEGA_COUNT = 5;
    private readonly int MINI_COUNT = 3;

    /// <summary>
    /// パックの種類の列挙体
    /// </summary>
    public enum packType
    {
        None = -1,
        JokerMega,
        Max,

    }

    /// <summary>
    /// パック事のアクションの配列
    /// </summary>
    List<System.Action> _packAction = new List<System.Action>();

    public void Awake()
    {
        instance = this;
        SetPackAction();

    }

    public void Start()
    {

        
    }

    public void GetPackAction(int id) 
    {
        _packAction[id]();
    }

    /// <summary>
    /// パックに合わせた関数をリストに追加する
    /// </summary>
    private void SetPackAction()
    {
        for (int i = 0; i < (int)packType.Max; i++)
        {
            switch ((packType)i)
            {
                case packType.JokerMega:
                    _packAction.Add(JokerMaga);
                    break;
            }





        }


    }

    private void JokerMaga()
    {

        //JokerUtility.ShopJoker(_leftPos.position,_rightPos.position,MEGA_COUNT);




    }




}
