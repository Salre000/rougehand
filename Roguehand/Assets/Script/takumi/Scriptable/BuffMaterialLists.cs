using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "BuffMaterialsObject", menuName = "ScriptableObjects/ BuffMaterialsList")]
public class BuffMaterialLists : ScriptableObject
{
    public List<Material> deckBuff = new((int)Card.deckBuff.MAX); 
    public List<Material> cardBuff = new((int)Card.cardBuff.MAX); 
    public List<Material> sealBuff = new((int)Card.sealBuff.MAX); 
    public List<Material> jokerBuff = new((int)Card.JokerBuff.MAX); 
}
