using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TitleDeckUI : MonoBehaviour
{
    public static TitleDeckUI instance;

    [SerializeField] private TextMeshProUGUI deckName;

    [SerializeField] private StringList deckNamses;

    public void Awake()
    {
        instance = this;
        deckName.text = deckNamses._expansion[0];
    }

    public void SetName(int ID) 
    {
        deckName.text = deckNamses._expansion[ID];

    }

}
