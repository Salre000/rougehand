using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 星座カードを纏めてこのクラス  
/// </summary>
public class ConstellationItem : ItemBase
{
    /// <summary>
    /// どんな星座なのかを表す変数
    /// </summary>
    private int _constellationID = -1;

    /// <summary>
    /// 星座の種類の列挙体
    /// </summary>
    public enum ConstellationType
    {
        None=-1,//不正値
        Ophiuchus,//ヘビ使い座
        Andromeda,//アンドロメダ座
        Lupus,//オオカミ座
        Cetus,//くじら座
        Pavo,//孔雀座
        Aries,//牡羊座
        Taurus,//牡牛座
        Gemini,//双子座
        Cancer,//蟹座
        Leo,//獅子座
        Virgo,//乙女座
        Libra,//天秤座
        Scorpius,//さそり座
        Sagittarius,//射手座
        Capricornus,//山羊座
        Aquarius,//水瓶座
        Pisces,//魚座
        MAX,//最大値
    }


    public override void Initializ()
    {
        // 特殊役は一度使用するまで星座カードに現れないためにこのような処理
        List<int> constellationIDList = new();
        List<int> roleCount = RoleManager.instance.GetRolePlayCountList();
        for (int i=0;i< (int)ConstellationType.MAX; i++) 
        {
            if (i<(int)RoleManager.Role.royalFlush&& roleCount[i]<=0) continue;
            constellationIDList.Add(i);

        }

        _constellationID = constellationIDList[Random.Range(0, constellationIDList.Count)];

        SetItemID(_constellationID);


    }

    public override void Use()
    {

        Debug.Log("星座カードが使用されたよ");
        // IDを使用して星座のレベルを上昇させる処理をかく
        RoleManager.instance.AddRoleLevel((RoleManager.Role)_constellationID);

        //星座カードを使用した事をJokerに知らせる
        JokerUtility.SetTraget(JokerActionUseEnum.JokerActionTarget.constellation);

    }
    // 星座カードの文字のID
    private readonly int ConstellationID = 1901;
    public override string GetTypes()
    {
        return MasterData.instance.GetStringMaster(ConstellationID);
    }
    

}
