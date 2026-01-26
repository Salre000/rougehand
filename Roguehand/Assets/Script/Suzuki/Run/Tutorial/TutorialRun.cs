using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRun : MonoBehaviour
{
    float _time = 0f;

    [SerializeField] TesChan _textGroup;
    [SerializeField] GameObject _clickCome;
    [SerializeField] GameObject _mesegePanel;
    [SerializeField] GameObject _mesegeObj;
    [SerializeField] TextMeshProUGUI _mesegeText;
    StringBuilder _builder = new StringBuilder();

    [SerializeField] List<GameObject> opImage;
    int opIndex = 0;

    int indexNumber = 0;
    string non = "";

    int max = 100;
    List<bool> fade = new List<bool>();
    float viewTime = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        // チュートリアルモードがオフならこのゲームオブジェクトを非表示にしてスクリプトを読ませなくする
        if (TitleStatic.GetDeckNumber() != 0/*!MemoryManager.GetTutorialFlag()*/)
        {
            this.gameObject.SetActive(false);
            return;
        }
        for (int i = 0; i < max; i++)
        {
            fade.Add(true);
        }
        fade[0] = false;
        _mesegePanel.SetActive(false);
        _mesegeObj.SetActive(false);
        _textGroup = GetComponent<TesChan>();
        for (int i = 0; i < opImage.Count; i++)
        {
            opImage[i].SetActive(false);
        }
        _clickCome.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Count();
        TutorialFade();
    }

    void Count()
    {
        _time += Time.deltaTime;
    }

    void TutorialFade()
    {
        Builder(non);
        TutorialText();
        TutorialPata();
        //TutorialFade2();
        //TutorialFade3();

    }

    void TutorialText()
    {
        if (fade[0]) return;
        if (_time >= 0.5f)
            _mesegePanel.SetActive(true);
        if (_time >= viewTime)
        {
            _mesegeObj.SetActive(true);
            _time = 0f;
            fade[0] = true;
            fade[1] = false;
        }
    }

    void TutorialPata()
    {
        if (fade[1]) return;
        if (_time >= viewTime)
        {
            // csvからメッセージを出す
            Builder(_textGroup.tutorialText[indexNumber]);
            _clickCome.SetActive(true);
            if (!ClickChack()) return;
            indexNumber++;
            _time = 0;
            _clickCome.SetActive(false);
            Asist();
        }

    }

    void Asist()
    {
        switch (indexNumber)
        {
            case 2: // 強い役
                opImage[opIndex].SetActive(true);
                break;
            case 4: // スコアを
                opImage[opIndex].SetActive(false);
                opIndex++;
                opImage[opIndex].SetActive(true);
                break;
            case 5: // プレイを
                opImage[opIndex].SetActive(false);
                opIndex++;
                opImage[opIndex].SetActive(true);
                break;
            case 6: // ハンドが
                opImage[opIndex].SetActive(false);
                opIndex++;
                opImage[opIndex].SetActive(true);
                break;
            case 8: // ゲームは
                opImage[opIndex].SetActive(false);
                break;
            case 10: // 報酬分
                opIndex++;
                opImage[opIndex].SetActive(true);
                break;
            case 11: // より好み
                opIndex++;
                opImage[opIndex].SetActive(true);
                break;
            case 12:// まずは
                opImage[opIndex - 1].SetActive(false);
                opImage[opIndex].SetActive(false);
                break;
            case 13: // タイトルに戻す
                GameSceneManager.LoadScene(GameSceneManager.titleScene);
                break;

            default:
                break;
        }
    }

    //void TutorialFade2()
    //{
    //    if (fade[1]) return;
    //    if (_time >= viewTime)
    //    {
    //        // ようこそ
    //        Builder(_textGroup.tutorialText[indexNumber]);
    //        if(!ClickChack())return;

    //        indexNumber++;
    //        _time = 0;
    //        fade[1] = true;
    //        fade[2] = false;
    //    }

    //}

    //void TutorialFade3()
    //{
    //    if (fade[2]) return;
    //    if (_time >= viewTime)
    //    {
    //        // ルールは
    //        Builder(_textGroup.tutorialText[indexNumber]);
    //        opImage[opIndex].SetActive(true);
    //        if (!ClickChack()) return;
    //        opImage[opIndex].SetActive(false);
    //        opIndex++;
    //        indexNumber++;
    //        _time = 0;
    //        fade[2] = true;
    //        fade[3] = false;
    //    }
    //}


    void Builder(string text)
    {
        _builder.Clear();
        _builder.Append(text);
        _mesegeText.text = _builder.ToString();
    }

    bool ClickChack()
    {
        return Input.GetMouseButtonDown(0);
    }
}
