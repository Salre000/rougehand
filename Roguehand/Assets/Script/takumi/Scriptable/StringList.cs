using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "stringObject", menuName = "ScriptableObjects/ StringList")]
public class StringList : ScriptableObject
{
    public List<string> _enumName = new List<string>();   
    public List<string> _expansion = new List<string>();
}

