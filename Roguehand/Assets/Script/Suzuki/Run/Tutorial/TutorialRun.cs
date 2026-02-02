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
    [SerializeField] GameObject _noClickPanel;
    [SerializeField] GameObject _fadePanel;
    [SerializeField] Transform _tutorialCanvas;
    [SerializeField] Transform _targetShopMessege;
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

    bool playFlag = false;
    bool oneShopFlag = false;

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
        // カードに触れなくさせる
        GrabManager.instance.SetGrabFlag(false);
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
        ShopTutorial();
    }

    void TutorialText()
    {
        if (playFlag) return;
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
        if (playFlag) return;
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
            case 0:

                break;
            case 2: // 強い役
                opImage[opIndex].SetActive(true);
                _fadePanel.SetActive(false);
                break;
            case 4: // スコアを
                DefaultCase();
                break;
            case 5:// ディスカードを押すと
                DefaultCase();
                break;
            case 6:// 選択したカードを捨て
                DefaultCase();
                break;
            case 7:// 捨てた枚数分だけ
                DefaultCase();
                break;
            case 8:// ディスカードのカウントが
                DefaultCase();
                break;
            case 10: // プレイを
                DefaultCase();
                break;
            case 11: // ハンドが
                DefaultCase();
                break;
            case 13: // ゲームは
                opImage[opIndex].SetActive(false);
                _fadePanel.SetActive(true);
                break;
            case 15: // 報酬分
                _fadePanel.SetActive(false);
                DefaultCase();
                break;
            case 16: // より好み
                DefaultCase();
                break;
            case 17:// まずは
                _fadePanel.SetActive(true);
                opImage[opIndex].SetActive(false);
                break;
            case 18: // カードの操作を可能にする
                opImage[opIndex].SetActive(false);
                _mesegePanel.SetActive(false);
                playFlag = true;
                _fadePanel.SetActive(false);
                _noClickPanel.SetActive(false);
                GrabManager.instance.SetGrabFlag(playFlag);
                break;
            case 23: // ジョーカー
                _fadePanel.SetActive(false);
                opIndex++;
                opImage[opIndex].SetActive(true);
                _noClickPanel.SetActive(true);
                break;
            case 24: // 星座
                DefaultCase();
                break;
            case 25: // それらが
                opImage[opIndex].SetActive(false);
                _fadePanel.SetActive(true);
                break;
            case 26: // パック
                _fadePanel.SetActive(false);
                opIndex++;
                opImage[opIndex].SetActive(true);
                break;
            case 28: // リロール
                DefaultCase();
                break;
            case 29: // 次のラウンド
                DefaultCase();
                break;
            case 30: // チュートリアルは
                opImage[opIndex].SetActive(false);
                _fadePanel.SetActive(true);
                break;
                case 32:
                GameSceneManager.LoadScene(GameSceneManager.titleScene);
                break;

            default:
                break;
        }
    }

    void ShopTutorial()
    {
        if (oneShopFlag) return;
        // ショップに移行した時
        if (!ShopManager.instance.IsShop()) return;
        _tutorialCanvas.localPosition = _targetShopMessege.localPosition;
        _tutorialCanvas.localRotation = _targetShopMessege.localRotation;
        _mesegePanel.SetActive(true);
        playFlag = false;
        _fadePanel.SetActive(true);
        _noClickPanel.SetActive(true);
        GrabManager.instance.SetGrabFlag(playFlag);
        oneShopFlag = true;
    }

    void DefaultCase()
    {
        opImage[opIndex].SetActive(false);
        opIndex++;
        opImage[opIndex].SetActive(true);
    }

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
