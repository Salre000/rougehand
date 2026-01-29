using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// オプションを開く閉じる

public class OptionOpen : MonoBehaviour
{
    [SerializeField] private Button _shopOpButton;
    [SerializeField] private Button _opButton;
    [SerializeField] private Button _opCloseButton;
    [SerializeField] private Button _resButton;
    [SerializeField] private Button _retireButton;
    [SerializeField] private Button _changeTitelButton;
    [SerializeField] private Button _editButton;
    [SerializeField] private GameObject _opObject;
    [SerializeField] private GameObject _opObjectEdit;
    [SerializeField] private Transform _gameTarget;
    [SerializeField] private Transform _shopTarget;
    // Start is called before the first frame update
    void Start()
    {
        _opButton.onClick.AddListener(Onclick);
        _opCloseButton.onClick.AddListener(Onclick);
        _shopOpButton.onClick.AddListener(OnShopOpClick);
        _resButton.onClick.AddListener(OnReset);
        _changeTitelButton.onClick.AddListener(OnChengeTitel);
        _editButton.onClick.AddListener(OpenEdit);
        _retireButton.onClick.AddListener(OnRetire);
        _opObject.SetActive(false);
        _opObjectEdit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Onclick()
    {
        if (_opObject.activeSelf)
        {
            _opObject.SetActive(false);
            GrabManager.instance.SetGrabFlag(true);
        }
        else
        {
            _opObject.transform.SetPositionAndRotation(_gameTarget.transform.position, _gameTarget.transform.rotation);
            _opObject.SetActive(true);
            GrabManager.instance.SetGrabFlag(false);
        }
    }
    void OnShopOpClick()
    {
        if (_opObject.activeSelf)
        {
            _opObject.SetActive(false);
            GrabManager.instance.SetGrabFlag(true);
        }
        else
        {
            _opObject.transform.SetPositionAndRotation(_shopTarget.transform.position, _shopTarget.transform.rotation);
            _opObject.SetActive(true);
            GrabManager.instance.SetGrabFlag(false);
        }
    }

    private void OnReset()
    {
        GameSceneManager.LoadScene(GameSceneManager.mainScene);
    }

    private void OnRetire()
    {
        GameSceneManager.LoadScene(GameSceneManager.titleScene);
        MemoryManager.Lost();
    }
    private void OnChengeTitel()
    {
        GameSceneManager.LoadScene(GameSceneManager.titleScene);
        MemoryManager.Keep();

    }
    private void OpenEdit() 
    {
       _opObjectEdit.SetActive(true);
    }
}
