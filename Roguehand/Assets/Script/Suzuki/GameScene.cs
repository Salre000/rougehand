using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// シーン管理
public static class GameSceneManager
{
    // シーンの名前
    public const string titleScene = "Title";
    public const string mainScene = "Main";

    // 普通のシーン遷移
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // モードを含めた普通のシーン遷移
    public static void LoadScene(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName, mode);
    }
}
