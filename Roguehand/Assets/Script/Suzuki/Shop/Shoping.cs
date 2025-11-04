using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ショップ状態がtrueになっているときの処理
public class Shoping : MonoBehaviour
{
    // ショップにカメラを向けさせるために必要
    [SerializeField] private Transform _vcam;
    // ショップへの向き
    private const float _TARGET_SHOP_CAM_ROTATE = -90f;
    // ランへの向き
    private const float _TARGET_RUN_CAM_ROTATE = 0.0f;
    float angle=0f;

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
        _vcam.rotation = Quaternion.RotateTowards(_vcam.rotation, Quaternion.Euler(_TARGET_SHOP_CAM_ROTATE, 0, 0), 1.0f);
        angle = NormalizeAngle(_vcam.eulerAngles.x);
        if ((Mathf.Abs(angle-_TARGET_SHOP_CAM_ROTATE))< 1)
            ShopManager.instance.SetIsShop(false);
    }

    private void ShopEnd()
    {
        // でバグ。
        if (Input.GetKeyDown(KeyCode.E))
            // ショップの終了
            ShopManager.instance.SetIsShop(false);

        if (ShopManager.instance.IsShop()) return;
        // ほとんど0ゼロならreturn
        angle = NormalizeAngle(_vcam.eulerAngles.x);
        if ((angle - _TARGET_RUN_CAM_ROTATE) < 0.001f) return;
        // ラン画面へ向く
        _vcam.rotation = Quaternion.RotateTowards(_vcam.rotation, Quaternion.Euler(_TARGET_RUN_CAM_ROTATE, 0, 0), 1.0f);

    }

    private float NormalizeAngle(float angle)
    {
        if (angle < 180f)
            angle -= 360f;
        return angle;
    }

}
