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
            return;
        }

        // ラン画面へ向く
        _vcam.rotation = Quaternion.Lerp(_vcam.rotation, Quaternion.Euler(_TARGET_RUN_CAM_ROTATE, 0, 0), Time.deltaTime * _camTime);

    }

    private void OnShopEnd()
    {
        if(!ShopManager.instance.IsShop()) return;

        // 次ラウンドへを押してショップを終了した
        ShopManager.instance.SetPushEndShop(true);
        ShopManager.instance.SetIsShop(false);
        // ラウンドのカウント数を増やす
        int roundCount=GameUtility.GetRoundCount();
        roundCount++;
        GameUtility.SetRoundCount(roundCount);
        // ラウンド数の反映
        TextUIManager.instance.SetRoundText(roundCount.ToString());
        
        // TODO:他にもリセットを仕込む必要がある
        // 手札の内部をリセット　
        CardManager.instance.ResetHand();
        // デッキの内部をリセット
        List<Card.Trump> dommyDeck = CardManager.instance.GetDeck();
        dommyDeck.GetAction(card=>
        {
            card.isSelect = false;
            card.state = Card.State.deck;
            return card;

        });

        // ドローの処理をリセット
        RoundObserver.Instance.RoundStartActions();

        //SaleObjectManager.instance.CreateRondom();

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
