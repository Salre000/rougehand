using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class ShopTextManager:MonoBehaviour
{
    public static ShopTextManager instance;
    private StringBuilder _builder = new();
    [SerializeField] private Transform _leftTargetPos;
    [SerializeField] private Transform _rightTargetPos;
    [SerializeField] private List<Transform> _beltUpText;
    [SerializeField] private List<Transform> _beltDownText;
    private float _speed = 0.5f;
    private float _posX = -200f;
    private float _plusX = 50f;
    [SerializeField]private TextMeshProUGUI _roleText;


    private void Awake()
    {
        if(instance == null)
            instance = this;
        InitializeText();
        Initialize();
    }

    private void Update()
    {
        BeltUpMove();
        BeltDownMove();
    }

    private void InitializeText()
    {
        _builder.Clear();
        _builder.Append("");
        SetRoleText(_builder.ToString());
    }

    private void Initialize()
    {

        Vector3 pos;
        for(int i=0;i< _beltUpText.Count;i++)
        {
            pos= _beltUpText[i].localPosition;
            pos.x = _posX;
            _beltUpText[i].localPosition = pos;
            _posX += _plusX;
        }
        for (int i = 0; i < _beltDownText.Count; i++)
        {
            pos = _beltDownText[i].localPosition;
            pos.x = _posX;
            _beltDownText[i].localPosition = pos;
            _posX -= _plusX;
        }
    }

    private void BeltUpMove()
    {
        for(int i=0;i< _beltUpText.Count;i++)
        {

            Vector3 vec = _beltUpText[i].localPosition;
            vec.x += _speed;
            _beltUpText[i].localPosition = vec;
            if((_beltUpText[i].localPosition-_rightTargetPos.localPosition).sqrMagnitude<0.1f)
                _beltUpText[i].localPosition=_leftTargetPos.localPosition;
        }
    }
    private void BeltDownMove()
    {
        for (int i = 0; i < _beltDownText.Count; i++)
        {

            Vector3 vec = _beltDownText[i].localPosition;
            vec.x -= _speed;
            _beltDownText[i].localPosition = vec;
            if ((_beltDownText[i].localPosition - _leftTargetPos.localPosition).sqrMagnitude < 0.1f)
                _beltDownText[i].localPosition = _rightTargetPos.localPosition;
        }
    }


    void SetRoleText(string value) { _roleText.text = value; }

}
