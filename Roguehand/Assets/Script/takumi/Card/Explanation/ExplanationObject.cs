using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExplanationObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _explanation;
    [SerializeField] Image _rarityColor;
    [SerializeField] TextMeshProUGUI _rarityText;
    [SerializeField]List<Image>_buffColorIcon=new List<Image>();
    [SerializeField]List<TextMeshProUGUI> _buffTextIcon = new List<TextMeshProUGUI>();
    [SerializeField]List<TextMeshProUGUI> _buffTextNameMini=new List<TextMeshProUGUI>();
    [SerializeField]List<TextMeshProUGUI> _buffTextMini = new List<TextMeshProUGUI>();
    [SerializeField] GameObject buffParent;

    public TextMeshProUGUI GetTextName() {  return _name; }
    public TextMeshProUGUI GetTextExplanation() {  return _explanation; }
    public TextMeshProUGUI GetTextRarityText() {  return _rarityText; }
    public Image GetTextRarityColor() {  return _rarityColor; }

    public TextMeshProUGUI GetBuffTextIcon(int ID) {  return _buffTextIcon[ID]; }
    public Image GetBuffColorIcon(int ID) {  return _buffColorIcon[ID]; }
    public TextMeshProUGUI GetBuffText(int ID) {  return _buffTextMini[ID]; }
    public TextMeshProUGUI GetBuffName(int ID) {  return _buffTextNameMini[ID]; }

    public GameObject GetBuffParent() { return buffParent; }

}
