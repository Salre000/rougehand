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

    void OnStart()
    {
        // 前回のセーブデータを消去
        MemoryManager.Lost();

        // デッキ番号をシーン持越し
        TitleStatic.SetDeckNumber(_deckMove.selectIndex);
        GameSceneManager.LoadScene(GameSceneManager.mainScene);
    }
}
