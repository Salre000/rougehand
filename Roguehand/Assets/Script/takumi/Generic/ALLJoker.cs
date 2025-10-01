using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
public static class ALLJoker
{public enum _allJokerEnum{
_ItemUseNeverUp,
_TESTJOKER,
_THISJOKER,
}
public static JokerBase GetJoker(_allJokerEnum joker){
        switch (joker){

            case _allJokerEnum._ItemUseNeverUp:return new ItemUseNeverUp();
            case _allJokerEnum._TESTJOKER:return new TESTJOKER();
            case _allJokerEnum._THISJOKER:return new THISJOKER();
}return null;}


}