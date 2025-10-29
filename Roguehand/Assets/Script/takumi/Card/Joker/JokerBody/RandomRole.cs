using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using UnityEngine;

public class RandomRole : JokerBase
{

    RoleManager.Role _role;

    float value = 0;

    public override void RoundStart()
    {
        _role = (RoleManager.Role)Random.Range(0, (int)RoleManager.Role.highCard);

    }
    public override void UpData()
    {

        if (JokerUtility.GetTargetRole() != _role) return;

        value = 12;

    }

    public override string GetExplanation()
    {
        StringBuilder sb = new StringBuilder();

        sb.Append("<size=18>");
        sb.Append("<color=#FF4040>");
        sb.Append(MasterData.instance.GetStringMaster(3000 + (int)_role));
        sb.Append("</color>");
        sb.Append(base.GetExplanation());
        sb.Append("</size>");


        return sb.ToString();
    }


    public override string GetExplanation2()
    {
        return Trun() < 1 ? string.Empty : MasterData.instance.GetStringMaster(1999) + Trun().ToString().GetRedString();
    }


    public override float Trun()
    {
        return value;
    }





}
