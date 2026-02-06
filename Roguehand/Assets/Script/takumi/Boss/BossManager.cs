using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BossManager : MonoBehaviour
{

    [SerializeField] Image bossIcon;

    private List<BossBase> bossBases = new List<BossBase>();

    IconList iconList;  
    public void Awake()
    {
        BossUtility.bossManager = this;

        iconList = Resources.Load<IconList>("takumi/iconObject");
        EndIcon();
    }

    public void Update()
    {
        if (bossBases.Count < 1) return;

        for (int i = 0; i < bossBases.Count; i++) bossBases[i].Update();
    }

    public void LateUpdate()
    {
        if (bossBases.Count < 1) return;

        for (int i = 0; i < bossBases.Count; i++) bossBases[i].LateUpdate();
    }



    public void BossEnd()
    {
        if (bossBases.Count < 1) return;

        for (int i = 0; i < bossBases.Count; i++) bossBases[i].End();

        bossBases.Clear();
        EndIcon();
    }



    public void CreateBoss(int id)
    {
        int _id = id + IDUtility.BOSS_ID;


        BossBase bossBase = null;

        switch (id)
        {
            case 0: bossBase = new TutorialBoss(); break;
            case 1: bossBase = new AlternativeBoss(); break;
            case 2: bossBase = new BanArt(); break;
            case 3: bossBase = new Castle(); break;
            case 4: bossBase = new Delete(); break;
            case 5: bossBase = new Equality(); break;
        }
        if (id != 0) VolumeManager.instance.UpBGM();

        bossBase.bossTextID = _id;
        bossBase.Initializ();

        SetBossIcon(id);

        bossBases.Add(bossBase);

    }

    public void CreateBoss(BossBase bossBase) 
    {
        bossBases.Add(bossBase);
        bossBase.BaseInitializ();
    }

    public void RandomCreateBoss()
    {
        CreateBoss(Random.Range(1, 6))
            ;


    }

    public BossBase GetBossBase() 
    {

        if(bossBases.Count<1)return null;
        return bossBases[0];
    }

    private void SetBossIcon(int ID) 
    {
        bossIcon.gameObject.SetActive(true);
        bossIcon.sprite = iconList._iconList[ID];
    }

    private void EndIcon() 
    {
        bossIcon.sprite = null;
        bossIcon.gameObject.SetActive(false);
    }


}
