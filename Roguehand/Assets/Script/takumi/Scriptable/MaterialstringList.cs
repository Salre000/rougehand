
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
[CreateAssetMenu(fileName = "MaterialsObject", menuName = "ScriptableObjects/ MaterialsList")]
public class MaterialstringList : ScriptableObject
{
    public List<Texture> _material = new List<Texture>();

}

