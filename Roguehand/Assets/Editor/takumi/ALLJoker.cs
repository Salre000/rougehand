using System.IO;
using System.Text;
using UnityEditor;
using UnityEngine;
public static class ALLJoker
{
    static JokerBase[] _allJoker = new JokerBase[]{
new ItemUseNeverUp(),
new ConstellationUseNeverUp(),
new ProbabilityDestruction(),
};
    public enum _allJokerEnum
    {
        _ItemUseNeverUp,
        _ConstellationUseNeverUp,
        _ProbabilityDestruction,
    }
    public static JokerBase GetJoker(int id) { return _allJoker[id]; }

    public static JokerBase[] GetJokerALL() { return _allJoker; }
}