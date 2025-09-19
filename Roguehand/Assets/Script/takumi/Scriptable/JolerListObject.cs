using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "JolerLists", menuName = "ScriptableObjects/ JolerListObject")]

public class JolerListObject : ScriptableObject
{
    public List<JokerBase> _jokerBases = new List<JokerBase>();

    public List<string> _className = new List<string>();

}

