using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXAnime : MonoBehaviour
{
    [SerializeField] RectTransform _panel;
    [SerializeField] Transform _fx;
    Vector3 _position = Vector3.zero;

    float _velocity = 0f;

    float _h_fade1 = 10.0f;
    float _w_fade1 = 2400.0f;

    bool fade1F=false;

    // Start is called before the first frame update
    void Start()
    {
        _position = _panel.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        PanelSavaTrans();
    }

    void Fade1()
    {
        _velocity=_panel.rect.height;
        _velocity = Mathf.Lerp(_panel.rect.height, _h_fade1, 1);
        // 垂直サイズの変更
        _panel.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, _velocity);

        if((_panel.rect.size.y-_h_fade1<0.1f))
            fade1F = true;
    }

    void Fade2()
    {
        if (!fade1F) return;

    }


    void PanelSavaTrans()
    {
        _panel.localPosition = _position;
    }
}
