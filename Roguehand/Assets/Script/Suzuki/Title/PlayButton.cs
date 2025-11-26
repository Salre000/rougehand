using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayButton : MonoBehaviour
{
    [SerializeField] Button _startButton;

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
        GameSceneManager.LoadScene(GameSceneManager.mainScene);
    }
}
