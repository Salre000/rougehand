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
    private bool _isComp = false;
    int allReward;
    float _time = 0f;
    float _endTime = 1f;

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
        
        _isComp = false;
        _isResultArrival = false;

    }

    private void ResetResultPosition()
    {
        if(!_isPush) return;

        MoneyFluctuation();

        // 終わったら下通す
        if (PlayManager.instance.IsFluctuation()) return;


        if (_time < _endTime)
        {
            _time += Time.deltaTime;
            return;
        }

        // ショップ画面へ向かせる
        ShopManager.instance.SetIsShop(true);

        GameUtility.SetIsRoundResult(false);

        // 元の位置に戻す
        Vector3 resultPosition = _clearResult.transform.localPosition;
        // 移動
        resultPosition = Vector3.Lerp(resultPosition, _resetLocalPosition, Time.deltaTime * _transTime);
        _clearResult.transform.localPosition = resultPosition;

        // 完了通知
        if ((resultPosition - _resetLocalPosition).sqrMagnitude < 0.01f)
        {
            _time = 0f;
            _isPush=false;
            _clearResult.SetActive(false);
        }
    }

    /// <summary>
    /// 所持金と報酬金の変動
    /// </summary>
    private void MoneyFluctuation()
    {
        // 一度だけ通す
        if (!_isComp)
        {
            // 報酬金の取得
            int reward = MasterData.instance.GetIntMaster(IDUtility.REWARD_ID + GameUtility.GetRoundCount());
            // 余った手数と合わせて合計金を算出
            allReward = GameUtility.GetHandCount() + reward;
            _isComp = true;
        }

        // 現在の所持金を取得
        int myMoney = GameUtility.GetMyMoney();
        // 現在の所持金をallRewardと合わせた数にする
        NumberFluctuation.FluctuationAnim(ref myMoney, myMoney + allReward, true);
        // 変動した所持金はしっかり受け取り元に返す
        GameUtility.SetMyMoney(myMoney);
        // テキストに反映
        _builder.Clear();
        _builder.Append("$");
        _builder.Append(myMoney);
        TextUIManager.instance.SetMoneyText(_builder.ToString());

        // 報酬金の変動 ゼロにする
        int reset = 0;
        NumberFluctuation.FluctuationAnim(ref allReward, reset, false);
        _builder.Clear();
        _builder.Append("$");
        _builder.Append(allReward);
        TextUIManager.instance.SetClearMoneyText(_builder.ToString());
    }

    /// <summary>
    /// ショップに移行が完了したタイミングでランのほうをリセットする
    /// </summary>
    void ResetShopEnd()
    {

    }
}
