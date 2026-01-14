using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// オプションを開く閉じる

public class OptionOpen : MonoBehaviour
{
    [SerializeField] private Button _opButton;
    [SerializeField] private GameObject _opObject;
    // Start is called before the first frame update
    void Start()
    {
        _opButton=GetComponent<Button>();
        _opButton.onClick.AddListener(Onclick);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Onclick()
    {
        if(_opObject.activeSelf)
            _opObject.SetActive(false);
        else
            _opObject.SetActive(true);
    }
}
