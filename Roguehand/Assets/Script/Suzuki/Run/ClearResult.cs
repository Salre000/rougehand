using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// ラウンドクリア後のリザルト表示
/// </summary>
public class ClearResult : MonoBehaviour
{
    private StringBuilder _builder = new StringBuilder();

    // リザルト関連
    [SerializeField] GameObject _clearResult;
    [SerializeField] Transform _targetcClearResult;
    private float _transTime = 5f;
    [SerializeField] Button _liquidationButton;
    private bool _isResultArrival = false;
    private Vector3 _resetLocalPosition;
    private bool _isPush = false;

    // Start is called before the first frame update
    void Awake()
    {
        _resetLocalPosition = _clearResult.transform.localPosition;
        _clearResult.SetActive(false);
        _liquidationButton.onClick.AddListener(OnLiquidation);

    }

    // Update is called once per frame
    void Update()
    {
        RoundClearCheck();
        ResetResultPosition();
    }

    /// <summary>
    /// リザルトを定位置に
    /// </summary>
    void RoundClearCheck()
    {
        if (!GameUtility.IsRoundResult()) return;
        _clearResult.SetActive(true);
        Vector3 resultPosition = _clearResult.transform.localPosition;
        // 移動
        resultPosition = Vector3.Lerp(resultPosition, _targetcClearResult.localPosition, Time.deltaTime * _transTime);
        _clearResult.transform.localPosition = resultPosition;

        // 完了通知
        if ((resultPosition - _targetcClearResult.localPosition).sqrMagnitude < 0.01f)
        {
            _isResultArrival=true;
        }

    }

    /// <summary>
    /// 清算ボタンが押されたとき
    /// </summary>
    private void OnLiquidation()
    {
        // 定位置につくまでボタンの発火を防ぐ
        if (!_isResultArrival) return;
        _isPush = true;
        ShopManager.instance.SetIsShop(true);
        _isResultArrival = false;

    }

    private void ResetResultPosition()
    {
        if(!_isPush) return;
        GameUtility.SetIsRoundResult(false);

        // 元の位置に戻す
        Vector3 resultPosition = _clearResult.transform.localPosition;
        // 移動
        resultPosition = Vector3.Lerp(resultPosition, _resetLocalPosition, Time.deltaTime * _transTime);
        _clearResult.transform.localPosition = resultPosition;

        // 完了通知
        if ((resultPosition - _resetLocalPosition).sqrMagnitude < 0.01f)
        {
            _isPush=false;
            _clearResult.SetActive(false);
        }
    }

    /// <summary>
    /// ショップに移行が完了したタイミングでランのほうをリセットする
    /// </summary>
    void ResetShopEnd()
    {

    }
}
