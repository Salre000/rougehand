using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FXAnime : MonoBehaviour
{
    [SerializeField] Button _startButton;
    [SerializeField] Button _backButton;
    [SerializeField] GameObject _gameObject;
    Transform _panel;
    [SerializeField] RectTransform _fx;
    [SerializeField] GameObject _fadePanel;
    Vector3 _position = Vector3.zero;


    float _h_fade1 = 1080.0f;
    float _w_fade1 = 2400.0f;

    float _time = 0;
    float _speed = 0.6f;

    bool _startF = false;
    bool fade1F = false;
    bool fade2F = false;



    // Start is called before the first frame update
    void Start()
    {
        _panel = _gameObject.transform;
        _startButton.onClick.AddListener(StartFlag);
        _backButton.onClick.AddListener(OnBack);
        _position = _panel.localPosition;
        _gameObject.SetActive(false);
        _fadePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (!_startF) return;
        FadeMove();
        PanelSavaTrans();
    }

    void StartFlag()
    {
        _startF = true;
        _fadePanel.SetActive(true);
        _gameObject.SetActive(true);
        VolumeManager.instance.PlaySystemSE();
    }

    void OnBack()
    {
        _gameObject.SetActive(false);
        _fadePanel.SetActive(false);
        _fx.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 0);
        _fx.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0);
        _time = 0;
        _startF = false; 
        fade1F = false;
    }

    void FadeMove()
    {
        _time += _speed * Time.deltaTime;
        Fade1();
        Fade2();
    }

    void Fade1()
    {
        if (fade1F) return;
        // 垂直サイズの変更
        _fx.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, Mathf.Lerp(_fx.rect.height, _h_fade1, _time));
        // 水平サイズの変更
        _fx.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, Mathf.Lerp(_fx.rect.width, _w_fade1, _time));


        if (Mathf.Abs(_fx.rect.height - _h_fade1) > 0.1f) return;
        if (Mathf.Abs(_fx.rect.width - _w_fade1) > 0.1f) return;
        fade1F = true;
        _time = 0;
    }

    void Fade2()
    {
        if (!fade1F || fade2F) return;

        
        fade2F = true;
        _time = 0;
    }


    void PanelSavaTrans()
    {
        _panel.localPosition = _position;
    }
}
