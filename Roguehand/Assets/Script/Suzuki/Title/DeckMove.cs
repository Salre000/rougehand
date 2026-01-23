using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeckMove : MonoBehaviour
{
    // デッキ選択をローテーションさせる

    [SerializeField] Button _leftButton;
    [SerializeField] Button _rightButton;

    [SerializeField] List<Transform> _tagePos;

    [SerializeField] List<Transform> _decks;

    float _speed = 90f;

    bool _leftF = false;
    bool _rightF = false;

    int _deckIndex = 0;
    int _tageIndex;
    int _default = 1;
    int _deckMaxIndex;
    int _reset = 0;
    int _magnitude = 5;

    int _selectMax = 3;
    public int selectIndex = 0;

    [SerializeField] TextMeshProUGUI _deckName;
    StringBuilder _builder = new StringBuilder();
    string _deckDefault = "○○○○";
    string _deckTutorial = "チュートリアル";

    // Start is called before the first frame update
    void Start()
    {
        _leftButton.onClick.AddListener(LeftFlag);
        _rightButton.onClick.AddListener(RightFlag);

        _tageIndex = _decks.Count - 1;
        _deckMaxIndex = _decks.Count - 1;
        _deckIndex = _deckMaxIndex;
        DeckName();

    }

    // Update is called once per frame
    void Update()
    {
        LeftLoopMove();
        RightLoopMove();
    }

    private void LeftFlag()
    {
        if (_leftF) return;
        _leftF = true;
        _deckIndex += _default;
        selectIndex++;
        if( selectIndex > _selectMax) 
            selectIndex = 0;
        if (_deckIndex > _deckMaxIndex) _deckIndex = _reset;
        DeckName();
    }
    private void RightFlag()
    {
        if (_rightF) return;
        _rightF = true;
        _deckIndex -= _default;
        selectIndex--;
        if (selectIndex < 0)
            selectIndex = _selectMax;
        if (_deckIndex < _reset) _deckIndex = _deckMaxIndex;
        DeckName();
    }


    private void LeftLoopMove()
    {
        if (!_leftF) return;
        _decks[_deckIndex].localPosition = Vector3.Lerp(_decks[_deckIndex].localPosition, _tagePos[_tageIndex].localPosition, _speed * Time.deltaTime);
        _deckIndex++;
        _tageIndex++;
        if (_deckIndex > _deckMaxIndex) _deckIndex = _reset;
        if (_tageIndex > _deckMaxIndex) _tageIndex = _reset;
        if ((_decks[_deckMaxIndex].localPosition - _tagePos[_tageIndex].localPosition).sqrMagnitude < _magnitude)
            _leftF = false;

    }
    private void RightLoopMove()
    {
        if (!_rightF) return;
        _decks[_deckIndex].localPosition = Vector3.Lerp(_decks[_deckIndex].localPosition, _tagePos[_tageIndex].localPosition, _speed * Time.deltaTime);
        _deckIndex--;
        _tageIndex--;
        if (_deckIndex < _reset) _deckIndex = _deckMaxIndex;
        if (_tageIndex < _reset) _tageIndex = _deckMaxIndex;
        if ((_decks[_deckMaxIndex].localPosition - _tagePos[_tageIndex].localPosition).sqrMagnitude < _magnitude)
            _rightF = false;

    }

    private void DeckName()
    {
        _builder.Clear();
        switch (selectIndex)
        {
            case 0:
                _builder.Append(_deckTutorial);
                break;
            default:
                _builder.Append(_deckDefault);
                break;
        }
        _deckName.text = _builder.ToString();
    }
}
