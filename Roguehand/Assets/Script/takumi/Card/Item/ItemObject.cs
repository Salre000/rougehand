using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{

    private Vector3 _lostPos= Vector3.zero;

    private readonly float EPSILON = 0.1f;

    private bool _isGrab = false;

    /// <summary>
    /// 時間経過の変数
    /// </summary>
    private float _time = 0;

    public void MovePos(Vector3 goal) 
    {

        if (_isGrab) 
        {
            //マウスポイント依存で座標を決定する
            Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(transform.position).z);
            transform.position = Camera.main.ScreenToWorldPoint(mousePos);

            return;
        }

        if (Vector3.Distance(goal,transform.position)< EPSILON) { _time = 0; _lostPos = transform.position;  return; }


        _time += Time.deltaTime;
        transform.position = Vector3.Lerp(_lostPos, goal, _time);




    }

    public void ResetTime() {_time = 0; _lostPos = transform.position; }


    public void SetGrab(bool flag) {  _isGrab = flag; _lostPos = transform.position; }

}
