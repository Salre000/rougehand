using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
public static class ALLJoker
{public enum _allJokerEnum{
_ConstellationUseNeverJoker,
        ItemUseNeverJoker,
_SixMinutesOne,
_ThousandMinutesOne,
}
public static JokerBase GetJoker(_allJokerEnum joker){
        switch (joker){

            case _allJokerEnum._ConstellationUseNeverJoker:return new ConstellationUseNeverJoker();
            case _allJokerEnum.ItemUseNeverJoker: return new ItemUseNeverJoker();
            case _allJokerEnum._SixMinutesOne:return new SixMinutesOne();
            case _allJokerEnum._ThousandMinutesOne:return new ThousandMinutesOne();
}return null;}


}