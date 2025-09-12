
using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    public static ShakeCamera Instance { get; private set; }

    private CinemachineVirtualCamera cam;
    private float shakeTimer;

    //���̃X�N���v�g���C���X�^���X��
    private void Awake()
    {
        Instance = this;
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    //���̊֐��ɗh��̋����Ɨh��鎞�Ԃ̈�����n��
    public void Shake(float intensity, float time)
    {
        cam = GetComponent<CinemachineVirtualCamera>();

        if (cam == null)
        {
            Debug.Log("CinemachineVirtualCamera���擾�ł��Ă��܂���B");
            return;
        }
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;
        shakeTimer = time;
    }

    void Update()
    {
        //�h�ꎞ�Ԃ��I�������h����~�߂�
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