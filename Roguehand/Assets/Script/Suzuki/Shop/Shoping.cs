using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ショップ状態がtrueになっているときの処理
public class Shoping : MonoBehaviour
{
    // ショップにカメラを向けさせるために必要
    [SerializeField] private Transform _vcam;
    private float _distance = 0.5f;
    // ショップへの向き
    private const float _TARGET_SHOP_CAM_ROTATE = 270;
    // ランへの向き
    private const float _TARGET_RUN_CAM_ROTATE = 0.0f;
    float angle = 0f;
    // カメラの補間移動時間
    private float _camTime = 8f;
    // ショップ終了ボタン
    [SerializeField] private Button _shopEndButton;
    private bool _shopCompFlag=false;

    private void Awake()
    {
        _shopEndButton.onClick.AddListener(OnShopEnd);
    }

    // Update is called once per frame
    void Update()
    {
        CamMove();
        ShopEnd();
    }
    /// <summary>
    /// カメラの向きをショップにスムーズに向かせます
    /// </summary>
    private void CamMove()
    {
        if (!ShopManager.instance.IsShop()) return;
        _vcam.rotation = Quaternion.Slerp(_vcam.rotation, Quaternion.Euler(_TARGET_SHOP_CAM_ROTATE, 0, 0), Time.deltaTime * _camTime);
        angle = NormalizeAngle(_vcam.eulerAngles.x);
        // 完全にショップを向いてから次へを押せるようにする
        if (angle - _TARGET_SHOP_CAM_ROTATE < 0.01f)
        {
            _shopCompFlag = true;

        }
    }

    private void ShopEnd()
    {
        if (ShopManager.instance.IsShop()) return;
        // ほとんど0ゼロならreturn
        angle = NormalizeAngle(_vcam.eulerAngles.x);
        if ((angle - _TARGET_RUN_CAM_ROTATE) < _distance)
        {
            // ラン画面に向ききったら終了お知らせフラグをリセット 
            ShopManager.instance.SetPushEndShop(false);
            _shopCompFlag = false;
            return;
        }

        // ラン画面へ向く
        _vcam.rotation = Quaternion.Lerp(_vcam.rotation, Quaternion.Euler(_TARGET_RUN_CAM_ROTATE, 0, 0), Time.deltaTime * _camTime);

    }

    private void OnShopEnd()
    {
        if(!ShopManager.instance.IsShop()) return;
        if(!_shopCompFlag)return;

        // 次ラウンドへを押してショップを終了した
        ShopManager.instance.SetPushEndShop(true);
        ShopManager.instance.SetIsShop(false);
        // ラウンドのカウントが3の場合にアンティのカウントを上げる
        int roundCount=GameUtility.GetRoundCount();
        if (roundCount >= 3)
        {
            int ante = GameUtility.GetAnteCount();
            ante++;
            GameUtility.SetAnteCount(ante);
            TextUIManager.instance.SetAnteText(ante.ToString());
            roundCount = 0;
        }
        // ボスであったらボスを消す
        BossManager.instance.BossEnd();

        // ボス戦を開始する
        if (roundCount == 2) 
        {

            BossManager.instance.RandomCreateBoss();

        }

        // ラウンドのカウント数を増やす
        roundCount++;
        GameUtility.SetRoundCount(roundCount);
        // ラウンド数の反映
        TextUIManager.instance.SetRoundText(roundCount.ToString());

        // 累計ラウンド数の増加
        int allRoundCount=GameUtility.GetAllRoundCount();
        allRoundCount++;
        GameUtility .SetAllRoundCount(allRoundCount);
        
        // TODO:他にもリセットを仕込む必要がある
        // 手札の内部をリセット　
        CardManager.instance.ResetHand();
        SaleObjectManager.instance.Clear();
        // デッキの内部をリセット
        List<Card.Trump> dommyDeck = CardManager.instance.GetDeck();
        dommyDeck.GetAction(card=>
        {
            card.isSelect = false;
            card.state = Card.State.deck;
            return card;

        });

        // デッキのオブジェクトをリセット
        CardObjectUtility.ResetCard();


        // ドローの処理をリセット
        RoundObserver.Instance.RoundStartActions();
        // 購入や売却の表示を全て削除
        SaleUtility.Claer(true);



    }

    private float _radius = 180f;
    private float _circumference = 360f;
    // 円周の正規化
    private float NormalizeAngle(float angle)
    {
        if (angle < _radius)
            angle -= _circumference;
        return angle;
    }

}
