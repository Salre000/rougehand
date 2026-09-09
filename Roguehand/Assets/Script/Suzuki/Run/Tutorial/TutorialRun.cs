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
        // 繝√Η繝ｼ繝医Μ繧｢繝ｫ繝｢繝ｼ繝峨′繧ｪ繝輔↑繧峨％縺ｮ繧ｲ繝ｼ繝繧ｪ繝悶ず繧ｧ繧ｯ繝医ｒ髱櫁｡ｨ遉ｺ縺ｫ縺励※繧ｹ繧ｯ繝ｪ繝励ヨ繧定ｪｭ縺ｾ縺帙↑縺上☆繧・
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
        // 繧ｫ繝ｼ繝峨↓隗ｦ繧後↑縺上＆縺帙ｋ
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
            // csv縺九ｉ繝｡繝・そ繝ｼ繧ｸ繧貞・縺・
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
            case 2: // 蠑ｷ縺・ｽｹ
                opImage[opIndex].SetActive(true);
                _fadePanel.SetActive(false);
                break;
            case 4: // 繧ｹ繧ｳ繧｢繧・
                DefaultCase();
                break;
            case 5:// 繝・ぅ繧ｹ繧ｫ繝ｼ繝峨ｒ謚ｼ縺吶→
                DefaultCase();
                break;
            case 6:// 驕ｸ謚槭＠縺溘き繝ｼ繝峨ｒ謐ｨ縺ｦ
                DefaultCase();
                break;
            case 7:// 謐ｨ縺ｦ縺滓椢謨ｰ蛻・□縺・
                DefaultCase();
                break;
            case 8:// 繝・ぅ繧ｹ繧ｫ繝ｼ繝峨・繧ｫ繧ｦ繝ｳ繝医′
                DefaultCase();
                break;
            case 10: // 繝励Ξ繧､繧・
                DefaultCase();
                break;
            case 11: // 繝上Φ繝峨′
                DefaultCase();
                break;
            case 13: // 繧ｲ繝ｼ繝縺ｯ
                opImage[opIndex].SetActive(false);
                _fadePanel.SetActive(true);
                break;
            case 15: // 蝣ｱ驟ｬ蛻・
                _fadePanel.SetActive(false);
                DefaultCase();
                break;
            case 16: // 繧医ｊ螂ｽ縺ｿ
                DefaultCase();
                break;
            case 17:// 縺ｾ縺壹・
                _fadePanel.SetActive(true);
                opImage[opIndex].SetActive(false);
                break;
            case 18: // 繧ｫ繝ｼ繝峨・謫堺ｽ懊ｒ蜿ｯ閭ｽ縺ｫ縺吶ｋ
                opImage[opIndex].SetActive(false);
                _mesegePanel.SetActive(false);
                playFlag = true;
                _fadePanel.SetActive(false);
                _noClickPanel.SetActive(false);
                GrabManager.instance.SetGrabFlag(playFlag);
                break;
            case 23: // 繧ｸ繝ｧ繝ｼ繧ｫ繝ｼ
                _fadePanel.SetActive(false);
                opIndex++;
                opImage[opIndex].SetActive(true);
                _noClickPanel.SetActive(true);
                break;
            case 24: // 譏溷ｺｧ
                DefaultCase();
                break;
            case 25: // 縺昴ｌ繧峨′
                opImage[opIndex].SetActive(false);
                _fadePanel.SetActive(true);
                break;
            case 26: // 繝代ャ繧ｯ
                _fadePanel.SetActive(false);
                opIndex++;
                opImage[opIndex].SetActive(true);
                break;
            case 28: // 繝ｪ繝ｭ繝ｼ繝ｫ
                DefaultCase();
                break;
            case 29: // 谺｡縺ｮ繝ｩ繧ｦ繝ｳ繝・
                DefaultCase();
                break;
            case 30: // 繝√Η繝ｼ繝医Μ繧｢繝ｫ縺ｯ
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
        // 繧ｷ繝ｧ繝・・縺ｫ遘ｻ陦後＠縺滓凾
        if (!ShopManager.instance.IsShop()) return;
        // Screen Space - Overlay 縺ｮ Canvas 縺ｧ縺ｯ Transform 縺ｮ菴咲ｽｮ繝ｻ蝗櫁ｻ｢縺ｯ謠冗判縺ｫ菴ｿ繧上ｌ縺ｪ縺・◆繧√・
        // 3D蛛ｴ縺ｮ逶ｮ蜊ｰ(_targetShopMessege)縺ｮ繝ｯ繝ｼ繝ｫ繝牙ｺｧ讓吶ｒ繧ｹ繧ｯ繝ｪ繝ｼ繝ｳ蠎ｧ讓吶↓螟画鋤縺励※繝｡繝・そ繝ｼ繧ｸ繝代ロ繝ｫ縺ｸ蜿肴丐縺吶ｋ
        Vector3 screenPos = Camera.main.WorldToScreenPoint(_targetShopMessege.position);
        _mesegePanel.GetComponent<RectTransform>().position = screenPos;
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

