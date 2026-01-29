using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossManager : MonoBehaviour
{

    public static BossManager instance;

    private List<BossBase> bossBases = new List<BossBase>();

    public void Awake()
    {
        instance = this;
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


        bossBase.bossTextID = _id;
        bossBase.Initializ();

        bossBases.Add(bossBase);

        bossBase.SetAction(() => { bossBases.Remove(bossBase); });
    }

    public void RandomCreateBoss()
    {
        CreateBoss(Random.Range(1, 5))
            ;


    }






}
