using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JokerObjectUtility 
{

    public static JokerObjectManager instance{ private get; set; }

    public static void NestJokerPlay(JokerObject  jokerObject) { instance.NestJokerPlay(jokerObject); } 

    public static void StartJokerPlay() {  instance.StartJokerPlay(); }

    public static void AddJoker(JokerBase jokerBase) { instance.AddJoker(jokerBase); }
    public static void RemoveJoker(int ID) { instance.RemoveJoker(ID); }
}