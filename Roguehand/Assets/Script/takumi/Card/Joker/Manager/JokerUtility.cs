using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class JokerUtility
{

    public static JokerManager instance { private get; set; }

    public static void Remove(JokerBase joker) { instance.Remove(joker); }

}