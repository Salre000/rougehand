using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class BossUtility
{

    public static BossManager bossManager;


    public static void CreateBoss(int id) {bossManager.CreateBoss(id);}
    public static void CreateBoss(BossBase id) {bossManager.CreateBoss(id);}

    public static void RandomCreateBoss() { bossManager.RandomCreateBoss();}
    public static BossBase GetBossBase() { return bossManager.GetBossBase();}
    public static void BossEnd() {bossManager.BossEnd();}


}
