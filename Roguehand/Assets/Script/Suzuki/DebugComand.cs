using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugComand : MonoBehaviour
{
    private int _score = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ScoreCountUP();
    }

    void ScoreCountUP()
    {
        if(!Input.GetKeyDown(KeyCode.Alpha1)) return;
        _score += 100;
        UIManager.SetRoundScereText(_score.ToString());
        Debug.Log(_score.ToString());
    }
}
