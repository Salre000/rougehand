using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using static Unity.VisualScripting.Member;

public class VolumeManager : MonoBehaviour
{
    public static VolumeManager instance;
    [SerializeField] private AudioMixer master;

    [SerializeField] private AudioClip TestBGM;
    [SerializeField] private AudioClip TestSE;
    [SerializeField] private AudioClip moneySE;
    

    private AudioSource BGMsource;
    private AudioSource SESource;
    private float masterVolume;
    private float BGMVolume;
    private float SEVolume;

    private float MixerRate = 80;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);

            return;
        }
        Initialize();

    }
    private void Initialize()
    {
        master.GetFloat("MasterVolume",out masterVolume);
        master.GetFloat("BGMVolume", out BGMVolume);
        master.GetFloat("SEVolume", out SEVolume);
        master.SetFloat("MasterVolume", masterVolume);
        master.SetFloat("BGMVolume", BGMVolume);
        master.SetFloat("SEVolume", SEVolume);
        //master.SetFloat("Master", masterVolume - 80);
        //master.SetFloat("BGM", BGMvolume - 80);
        //master.SetFloat("SE", SEvolume - 80);
        BGMsource=gameObject.AddComponent<AudioSource>();
        SESource=gameObject.AddComponent<AudioSource>();
        BGMsource.outputAudioMixerGroup = master.FindMatchingGroups("Master")[2];
        SESource.outputAudioMixerGroup = master.FindMatchingGroups("Master")[1];
        BGMsource.clip = TestBGM;
        BGMsource.loop = true;
        BGMsource.Play();

    }

    public void ChengeMaster(float value) 
    {
        masterVolume = value- MixerRate;

        master.SetFloat("MasterVolume", masterVolume);

    }
    public void ChengeBGM(float value) 
    {
        BGMVolume = value- MixerRate;

        master.SetFloat("BGMVolume", BGMVolume);

    }
    public void ChengeSE(float value) 
    {
        SEVolume = value- MixerRate;

        master.SetFloat("SEVolume", SEVolume);

    }

    public float GetMaster() {  return masterVolume+ MixerRate; }
    public float GetBGM() {  return BGMVolume+ MixerRate; }
    public float GetSE() {  return SEVolume+ MixerRate; }
    public void PlayScoreSE() 
    {
        SESource.pitch = GameConfig.GetGameSpeed();
        SESource.PlayOneShot(TestSE,1);
    }
    public void PlayMoneySE() 
    {
        SESource.pitch =5f;
        SESource.clip = moneySE;
        SESource.Play();
    }
    public void PlayMoneyShop() 
    {
        SESource.pitch = 0.5f;
        SESource.PlayOneShot(moneySE);

    }
    public void EndSE() 
    {

        SESource.Stop();

    }
}
