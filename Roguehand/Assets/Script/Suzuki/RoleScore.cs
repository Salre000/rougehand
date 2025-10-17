using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoleManager;

public class RoleScore : MonoBehaviour
{
    private int _basic = 0; // äÓñ{
    private int _magni = 0; // î{ó¶
    private int _score = 0; // çáåv

    private void RoleScoreCheck()
    {
        RoleManager.Role role= instance.GetRole();
        switch (role)
        {
            default:
                break;
        }
    }


}
