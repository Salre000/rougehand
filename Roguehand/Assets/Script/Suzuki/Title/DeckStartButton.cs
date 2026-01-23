using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckStartButton : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] DeckMove _deckMove;

    private void Awake()
    {
        _startButton.onClick.AddListener(OnStart);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnStart()
    {
        // デッキ番号をシーン持越し
        TitlStatic.SetDeckNumber(_deckMove.selectIndex);
        GameSceneManager.LoadScene(GameSceneManager.mainScene);
    }
}
