using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckMove : MonoBehaviour
{
    // デッキ選択をローテーションさせる

    [SerializeField] Button _leftButton;
    [SerializeField] Button _rightButton;

    [SerializeField] List<Transform> _tagePos;

    [SerializeField] List<Transform> _decks;

    float _speed = 50;

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


    // Start is called before the first frame update
    void Start()
    {
        _leftButton.onClick.AddListener(LeftFlag);
        _rightButton.onClick.AddListener(RightFlag);

        _tageIndex = _decks.Count - 1;
        _deckMaxIndex = _decks.Count - 1;
        _deckIndex = _deckMaxIndex;
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
        Debug.Log("現在選択されているデッキ番号: " + selectIndex);
        if (_deckIndex > _deckMaxIndex) _deckIndex = _reset;
    }
    private void RightFlag()
    {
        if (_rightF) return;
        _rightF = true;
        _deckIndex -= _default;
        selectIndex--;
        if (selectIndex < 0)
            selectIndex = _selectMax;
        Debug.Log("現在選択されているデッキ番号: " + selectIndex);
        if (_deckIndex < _reset) _deckIndex = _deckMaxIndex;
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
}
