using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
public static class ALLJoker
{
    public enum _allJokerEnum
    {
        _ConstellationUseNeverJoker,
        _Seraph,
        _ItemUseNeverJoker,
        _SixMinutesOne,
        _ThousandMinutesOne,
        _LostColor,
        _IncreaseTheSellingPrice,
        _BuffJoker,
        _RandomRole,
        MAX
    }
    public static JokerBase GetJoker(_allJokerEnum joker)
    {
        JokerBase jokerBase = null;
        switch (joker)
        {

            case _allJokerEnum._ConstellationUseNeverJoker: jokerBase = new ConstellationUseNeverJoker(); break;
            case _allJokerEnum._Seraph: jokerBase = new Seraph(); break;
            case _allJokerEnum._ItemUseNeverJoker: jokerBase = new ItemUseNeverJoker(); break;
            case _allJokerEnum._SixMinutesOne: jokerBase = new SixMinutesOne(); break;
            case _allJokerEnum._ThousandMinutesOne: jokerBase = new ThousandMinutesOne(); break;
            case _allJokerEnum._LostColor: jokerBase = new LostColor(); break;
            case _allJokerEnum._IncreaseTheSellingPrice: jokerBase = new IncreaseTheSellingPrice(); break;
            case _allJokerEnum._BuffJoker: jokerBase = new BuffJoker(); break;
            case _allJokerEnum._RandomRole: jokerBase = new RandomRole(); break;
        }

        jokerBase?.SetID((int)joker + 1 + 2000);
        
        return jokerBase;
    }


}