using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UISaleValueObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _valueText;
    
    public void SetValue(float value) 
    {
        _valueText.text = "$"+value.ToString();

    }
}
