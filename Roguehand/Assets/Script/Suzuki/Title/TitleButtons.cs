using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleButtons : MonoBehaviour
{
    [SerializeField] Button _continueButton;
    [SerializeField] Button _exitButton;


    private void Awake()
    {
        _continueButton.onClick.AddListener(OnContinue);
        _exitButton.onClick.AddListener(OnExit);

    }


    void OnContinue()
    {
        //GameSceneManager.LoadScene(GameSceneManager.mainScene);
    }

    void OnExit()
    {

    }
}
