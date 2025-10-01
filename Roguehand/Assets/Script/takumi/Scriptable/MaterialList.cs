
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
[CreateAssetMenu(fileName = "MaterialObject", menuName = "ScriptableObjects/ MaterialList")]
public class MaterialList : ScriptableObject
{
    public List<Texture> _materialS = new List<Texture>();
    public List<Texture> _materialH = new List<Texture>();
    public List<Texture> _materialD = new List<Texture>();
    public List<Texture> _materialC = new List<Texture>();
}

