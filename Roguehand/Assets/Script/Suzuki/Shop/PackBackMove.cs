using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// パック購入時専用背景が移動、パック終了時にも移動
/// </summary>
public class PackBackMove : MonoBehaviour
{
    [SerializeField] private GameObject _packBack;
    [SerializeField] private Transform _targetPos;
    private Vector3 _originPos=new();
    private float _time = 0.05f;

    private void Awake()
    {
        _originPos=Vector3.zero;
        _packBack.SetActive(false);

    }

    void Update()
    {
        BuyPackBackMove();
        EndPack();
    }

    /// <summary>
    /// 購入時背景が来る
    /// </summary>
    private void BuyPackBackMove()
    {
        if(!PackManager.instance.IsBuyPack()) return;
        if ((_packBack.transform.localPosition - _originPos).sqrMagnitude < 0.001f) return;
        _packBack.SetActive(true);
        _packBack.transform.localPosition = Vector3.Lerp(_packBack.transform.localPosition, _originPos, _time);
        
    }
    /// <summary>
    /// パック終了時背景が帰る
    /// </summary>
    private void EndPack()
    {
        if (PackManager.instance.IsBuyPack()) return;
        if (!_packBack.activeSelf) return;
        if ((_packBack.transform.localPosition - _targetPos.localPosition).sqrMagnitude < 0.001f)
        {
            _packBack.SetActive(false);
            return;
        }
        _packBack.transform.localPosition = Vector3.Lerp(_packBack.transform.localPosition, _targetPos.localPosition, _time);

    }
}
