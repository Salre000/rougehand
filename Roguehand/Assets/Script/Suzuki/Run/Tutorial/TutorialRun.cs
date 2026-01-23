using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialRun : MonoBehaviour
{
    float _time = 0f;

    [SerializeField] TextGroup _textGroup;
    [SerializeField] GameObject _mesegePanel;
    [SerializeField] GameObject _mesegeObj;
    [SerializeField] TextMeshProUGUI _mesegeText;
    StringBuilder _builder = new StringBuilder();

    bool fade1 = false;
    bool fade2 = false;
    bool fade3 = false;

    // Start is called before the first frame update
    void Start()
    {
        // チュートリアルモードがオフならこのゲームオブジェクトを非表示にしてスクリプトを読ませなくする
        if (TitleStatic.GetDeckNumber() != 0/*!MemoryManager.GetTutorialFlag()*/)
        {
            this.gameObject.SetActive(false);
            return;
        }
        _mesegePanel.SetActive(false);
        _mesegeObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Count();
        TutorialText();
        TutorialFade2();
    }

    void Count()
    {
        _time += Time.deltaTime;
    }
    void TutorialText()
    {
        if (fade1) return;
        if (_time >= 0.5f)
            _mesegePanel.SetActive(true);
        if (_time >= 1f)
        {
            _mesegeObj.SetActive(true);
            _time = 0f;
            fade1 = true;
        }
    }

    void TutorialFade2()
    {
        if (!fade1 || fade2) return;
        if (_time >= 1f)
        {
            Builder(_textGroup.tutorial1);
            fade2 = true;
        }
    }

    void Builder(string text)
    {
        _builder.Clear();
        _builder.Append(text);
        _mesegeText.text = _builder.ToString();
    }
}
