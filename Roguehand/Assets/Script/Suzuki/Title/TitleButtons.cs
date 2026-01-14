using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButtons : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _continueButton;
    [SerializeField] Button _exitButton;
    [SerializeField] GameObject _fadePanel;
    [SerializeField] GameObject _selectDeckPanel;

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStart);
        _continueButton.onClick.AddListener(OnContinue);
        _exitButton.onClick.AddListener(OnExit);
        _fadePanel.SetActive(false);
        _selectDeckPanel.SetActive(false);
    }

    void OnStart()
    {
        _fadePanel.SetActive(true);
        _selectDeckPanel.SetActive(true);
    }

    void OnContinue()
    {
        //GameSceneManager.LoadScene(GameSceneManager.mainScene);
    }

    void OnExit()
    {

    }
}
