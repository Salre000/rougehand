using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
public static class ALLJoker
{public enum _allJokerEnum{
_ConstellationUseNeverJoker,
_Seraph,
_ItemUseNeverJoker,
_SixMinutesOne,
_ThousandMinutesOne,
_LostColor,
}
public static JokerBase GetJoker(_allJokerEnum joker){
        switch (joker){

            case _allJokerEnum._ConstellationUseNeverJoker:return new ConstellationUseNeverJoker();
            case _allJokerEnum._Seraph:return new Seraph();
            case _allJokerEnum._ItemUseNeverJoker:return new ItemUseNeverJoker();
            case _allJokerEnum._SixMinutesOne:return new SixMinutesOne();
            case _allJokerEnum._ThousandMinutesOne:return new ThousandMinutesOne();
            case _allJokerEnum._LostColor:return new LostColor();
}return null;}


}