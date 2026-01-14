using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleStartButton : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] GameObject _fadePanel;
    [SerializeField] GameObject _selectDeckPanel;

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStart);
        _fadePanel.SetActive(false);
        _selectDeckPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStart()
    {
        _fadePanel.SetActive(true);
        _selectDeckPanel.SetActive(true);
        //GameSceneManager.LoadScene(GameSceneManager.mainScene);
    }
}
