using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TitleButtons : MonoBehaviour
{
    [SerializeField] Button _continueButton;
    [SerializeField] Button _exitButton;


    private void Awake()
    {
        _exitButton.onClick.AddListener(OnExit);

        // セーブデータが存在していたら返す
        if (MemoryManager.CheckSaveDeta())
        {
            _continueButton.onClick.AddListener(OnContinue);
            return;
        }

        // 色を灰色に変更

        TextMeshProUGUI text= _continueButton.GetComponent<TextMeshProUGUI>();

        text.color = Color.gray;



    }


    void OnContinue()
    {
        GameSceneManager.LoadScene(GameSceneManager.mainScene);

    }

    void OnExit()
    {
        Application.Quit();

    }
}
