using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JokerUtility
{

    public static JokerManager instance { private get; set; }

    public static void Remove(JokerBase joker) { instance.Remove(joker); }

    public static JokerActionUseEnum.JokerActionTarget GetTarget() { return instance.GetTarget(); }

    public static void AddMagnification(float magnification) {instance.JokerAddMagnification(magnification);}

    public static void Addjoker(int ID) { instance.AddJoker(ID); }

    public static void JokerPlayStart() { JokerObjectUtility.StartJokerPlay(); }

    public static void RoundStartJoker() { instance.RoundStart(); }
    public static void RoundEndJoker() { instance.RoundEnd(); }

}