using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
public static class ALLJoker
{public enum _allJokerEnum{
_ItemUseNeverUp,
_TESTS,
}
public static JokerBase GetJoker(_allJokerEnum joker){
        switch (joker){

            case _allJokerEnum._ItemUseNeverUp:return new ItemUseNeverUp();
            case _allJokerEnum._TESTS:return new TESTS();
}return null;
    }


}