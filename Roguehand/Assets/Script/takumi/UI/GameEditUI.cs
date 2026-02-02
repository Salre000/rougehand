using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEditUI : MonoBehaviour
{
    [SerializeField] private Slider _master;
    [SerializeField] private Slider _BGM;
    [SerializeField] private Slider _SE;

    [SerializeField] private Button _endButton;
    [SerializeField] private GameObject onObjectedit;


    public void Awake()
    {
        _master.value = VolumeManager.instance.GetMaster();
        _BGM.value = VolumeManager.instance.GetBGM();
        _SE.value = VolumeManager.instance.GetSE();
        _endButton.onClick.AddListener(() => {
            VolumeManager.instance.PlaySystemSE();
            onObjectedit.SetActive(false); });
        _master.onValueChanged.AddListener(ChengeVolumeMaster);
        _BGM.onValueChanged.AddListener(ChengeVolumeBGM);
        _SE.onValueChanged.AddListener(ChengeVolumeSE);
    }

    private void ChengeVolumeMaster(float value) 
    {

        VolumeManager.instance.ChengeMaster(value);


    }
    private void ChengeVolumeBGM(float value) 
    {

        VolumeManager.instance.ChengeBGM(value);


    }
    private void ChengeVolumeSE(float value) 
    {

        VolumeManager.instance.ChengeSE(value);


    }


}
