using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeckMove : MonoBehaviour
{
    [SerializeField] Button _leftButton;
    [SerializeField] Button _rightButton;

    [SerializeField] List<Transform> _tagePos;

    [SerializeField] List<Transform> _decks;
    int _index;
    int _count=0;
    int _countOn=0;
    float _speed=4;

    bool _leftF=false;
    bool _rightF=false;

    // Start is called before the first frame update
    void Start()
    {
        _leftButton.onClick.AddListener(LefFlag);
        _rightButton.onClick.AddListener(RigFlag);
        _index=_decks.Count;
        _count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        LeftMove();
        RightMove();
    }

    private void LefFlag()
    {
        _leftF=true;
    }
    private void RigFlag()
    {
        _rightF=true;
    }

    private void LeftMove()
    {
        if(!_leftF) return;
        _decks[0].localPosition = Vector3.Slerp(_decks[0].localPosition, _tagePos[3].localPosition, _speed * Time.deltaTime);
        _decks[1].localPosition = Vector3.Slerp(_decks[1].localPosition, _tagePos[0].localPosition, _speed * Time.deltaTime);
        _decks[2].localPosition = Vector3.Slerp(_decks[2].localPosition, _tagePos[1].localPosition, _speed * Time.deltaTime);
        _decks[3].localPosition = Vector3.Slerp(_decks[3].localPosition, _tagePos[2].localPosition, _speed * Time.deltaTime);

        _leftF =false;
    }
    private void RightMove()
    {
        if (!_rightF) return;


    }
}
