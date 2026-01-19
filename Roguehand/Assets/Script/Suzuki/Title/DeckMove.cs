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

    float _speed=50;

    bool _leftF=false;
    bool _rightF=false;

    int _deckIndex = 0;
    int _tageIndex;
    int _default = 1;
    int _deckMAX;
    int _reset = 0;
    int _magnitude = 5;

    // Start is called before the first frame update
    void Start()
    {
        _leftButton.onClick.AddListener(LefFlag);
        _rightButton.onClick.AddListener(RigFlag);

        _tageIndex = _decks.Count - 1;
        _deckMAX = _decks.Count - 1;
        _deckIndex = _deckMAX;
    }

    // Update is called once per frame
    void Update()
    {
        LeftLoopMove();
        RightLoopMove();
    }

    private void LefFlag()
    {
        if(_leftF) return;
        _leftF=true;
        _deckIndex += _default;
        if (_deckIndex > _deckMAX) _deckIndex = _reset;
    }
    private void RigFlag()
    {
        if(_rightF) return;
        _rightF=true;
        _deckIndex -= _default;
        if (_deckIndex < _reset) _deckIndex = _deckMAX;
    }


    private void LeftLoopMove()
    {
        if (!_leftF) return;
        _decks[_deckIndex].localPosition = Vector3.Lerp(_decks[_deckIndex].localPosition, _tagePos[_tageIndex].localPosition, _speed * Time.deltaTime);
        _deckIndex++;
        _tageIndex++;
        if (_deckIndex > _deckMAX) _deckIndex = _reset;
        if (_tageIndex > _deckMAX) _tageIndex = _reset;
        if ((_decks[_deckMAX].localPosition - _tagePos[_tageIndex].localPosition).sqrMagnitude<_magnitude)
            _leftF = false;

    }
    private void RightLoopMove()
    {
        if (!_rightF) return;
        _decks[_deckIndex].localPosition = Vector3.Lerp(_decks[_deckIndex].localPosition, _tagePos[_tageIndex].localPosition, _speed * Time.deltaTime);
        _deckIndex--;
        _tageIndex--;
        if (_deckIndex < _reset) _deckIndex = _deckMAX;
        if (_tageIndex < _reset) _tageIndex = _deckMAX;
        if ((_decks[_deckMAX].localPosition - _tagePos[_tageIndex].localPosition).sqrMagnitude<_magnitude)
            _rightF = false;

    }
}
