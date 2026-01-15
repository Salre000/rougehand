using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// オプションを開く閉じる

public class OptionOpen : MonoBehaviour
{
    [SerializeField] private Button _opButton;
    [SerializeField] private Button _opCloseButton;
    [SerializeField] private Button _resButton;
    [SerializeField] private Button _retireButton;
    [SerializeField] private GameObject _opObject;
    // Start is called before the first frame update
    void Start()
    {
        _opButton.onClick.AddListener(Onclick);
        _opCloseButton.onClick.AddListener(Onclick);
        _resButton.onClick.AddListener(OnReset);
        _retireButton.onClick.AddListener(OnRetire);
        _opObject.SetActive(false);
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

    private void OnReset()
    {
        GameSceneManager.LoadScene(GameSceneManager.mainScene);
    }

    private void OnRetire()
    {
        GameSceneManager.LoadScene(GameSceneManager.titleScene);

    }
}
