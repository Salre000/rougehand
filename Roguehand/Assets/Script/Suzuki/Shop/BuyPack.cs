using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// パック購入時
/// </summary>
public class BuyPack:MonoBehaviour
{
    [SerializeField] private Transform _packMoveTarget;
    private GameObject _pickPack;
    private float _time = 3.0f;
    private float _distance = 0.001f;

    private void Update()
    {
        Buy();
    }

    private void Buy()
    {
        if (!PackManager.instance.IsBuyPack()) return;
        if (PackManager.instance.IsArrival()) return;
        PickGet();
        Move();
    }

    /// <summary>
    /// 選択したパックを中央に移動させる
    /// </summary>
    private void Move()
    {
        Vector3 value = _pickPack.transform.localPosition;
        value = Vector3.Lerp(value, _packMoveTarget.localPosition, _time);
        _pickPack.transform.localPosition = value;

        // パックが目的地にある程度近づいたか
        if ((_pickPack.transform.localPosition - _packMoveTarget.transform.localPosition).sqrMagnitude > _distance) return;
        PackManager.instance.SetIsArrival(true);
    }

    /// <summary>
    /// 選択されたパックを取得
    /// </summary>
    private void PickGet()
    {
        if (_pickPack != null) return;
        _pickPack=PackManager.instance.GetPickPack();
    }
}
