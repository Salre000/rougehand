using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
public static class ALLJoker
{static JokerBase[] _allJoker = new JokerBase[]{
new Test1(),
new Test2(),
};
public enum _allJokerEnum{
_Test1,
_Test2,
}
public static JokerBase GetJoker(int id){ return JokerBase[id];}

public static JokerBase[] GetJokerALL(){ return JokerBase;}
}