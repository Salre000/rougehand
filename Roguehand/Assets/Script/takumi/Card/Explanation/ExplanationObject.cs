using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _explanation;
    [SerializeField] TextMeshProUGUI _explanation2;
    [SerializeField] Image _rarityColor;
    [SerializeField] TextMeshProUGUI _rarityText;
    [SerializeField]List<Image>_buffColorIcon=new List<Image>();
    [SerializeField]List<TextMeshProUGUI> _buffTextIcon = new List<TextMeshProUGUI>();
    [SerializeField]List<TextMeshProUGUI> _buffTextNameMini=new List<TextMeshProUGUI>();
    [SerializeField]List<TextMeshProUGUI> _buffTextMini = new List<TextMeshProUGUI>();

    public TextMeshProUGUI GetTextName() {  return _name; }
    public TextMeshProUGUI GetTextExplanation() {  return _explanation; }
    public TextMeshProUGUI GetTextExplanation2() {  return _explanation2; }
    public TextMeshProUGUI GetTextRarityText() {  return _rarityText; }
    public Image GetTextRarityColor() {  return _rarityColor; }

    public TextMeshProUGUI GetBuffTextIcon(int ID) {  return _buffTextIcon[ID]; }
    public Image GetBuffColorIcon(int ID) {  return _buffColorIcon[ID]; }
    public TextMeshProUGUI GetBuffText(int ID) {  return _buffTextMini[ID]; }
    public TextMeshProUGUI GetBuffName(int ID) {  return _buffTextNameMini[ID]; }



}
