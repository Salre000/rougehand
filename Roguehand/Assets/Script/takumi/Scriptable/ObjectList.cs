using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "ObjectList", menuName = "ScriptableObjects/ ObjectList")]
public class ObjectList : ScriptableObject
{
    public List<GameObject> _Objects = new List<GameObject>();
    public List<string> _expansion = new List<string>();
}
