using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// �V�[���Ǘ�
public static class GameSceneManager
{
    // �V�[���̖��O
    public const string titleScene = "TitleScene";
    public const string mainScene = "MainGame";
    public const string selectScene = "SelectScene";
    public const string resultScene = "ResultScene";
    public const string loadScene = "LoadScene";
    public const string changeScene = "ChangeScene";

    // �Z���N�g�V�[������^�C�g���ɖ߂�Ƃ��ɂ����g���ϐ�
    public static bool isTargetTitle = false;

    // ���ʂ̃V�[���J��
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // ���[�h���܂߂����ʂ̃V�[���J��
    public static void LoadScene(string sceneName, LoadSceneMode mode)
    {
        SceneManager.LoadScene(sceneName, mode);
    }
}
