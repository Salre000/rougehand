using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerManager : MonoBehaviour
{
    /// <summary>
    /// �W���[�J�[���܂Ƃ߂����X�g
    /// </summary>
    private List<JokerBase> _jokers;

    public void Awake()
    {
        
    }

    /// <summary>
    /// �W���[�J�[��j������֐�
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
    /// �W���[�J�[��ǉ�����֐�
    /// </summary>
    /// <param name="ID"></param>
    public void AddJoker(int ID) 
    {

    }



}
