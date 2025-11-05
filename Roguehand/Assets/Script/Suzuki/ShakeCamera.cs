
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera Instance { get; private set; }

    private CinemachineVirtualCamera cam;
    private float shakeTimer;

    //このスクリプトをインスタンス化
    private void Awake()
    {
        Instance = this;
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    /// <summary>
    /// vcamを使用してカメラを揺らす
    /// </summary>
    /// <param name="intensity">揺れ度</param>
    /// <param name="time">揺れてる時間</param>
    public void Shake(float intensity, float time)
    {
        cam = GetComponent<CinemachineVirtualCamera>();

        if (cam == null)
        {
            Debug.Log("CinemachineVirtualCameraが取得できていません。");
            return;
        }
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    void Update()
    {
        //揺れ時間が終わったら揺れを止める
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = 0f;
            }
        }
    }
}