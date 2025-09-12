using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{

    /// <summary>
    /// ‚±‚ÌƒJ[ƒh‚Ìó‘Ô
    /// </summary>
    public enum status 
    {
        none=-1,
        deck,
        hand,
        trash
    }

    private status _status = status.none;


    public void initialize() 
    {


    }

    public void SetStatus(status status) {_status = status;}

    public status GetStatus() { return _status; }
}
