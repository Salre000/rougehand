using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "iconObject", menuName = "ScriptableObjects/ IconList")]

public class IconList : ScriptableObject
{
    public List<Sprite> _iconList = new List<Sprite>();
}
