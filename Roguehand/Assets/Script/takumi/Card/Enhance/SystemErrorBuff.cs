using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;

public class SystemErrorBuff
{


    private List<Errorbuff> _errorList = new List<Errorbuff>();

    [StructLayout(LayoutKind.Sequential)]
    private struct POINT
    {
        public int X;
        public int Y;
    }
    /// <summary>
    /// マウスカーソルを移動させるウィンドウズの関数を引っ張ってくる
    /// </summary>
    /// <param name="X"></param>
    /// <param name="Y"></param>
    /// <returns></returns>
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int X, int Y);


    [DllImport("user32.dll")]
    private static extern bool GetCursorPos(out POINT lpPoint);

    public void UpData()
    {
        for (int i = 0; i < _errorList.Count; i++)
            _errorList[i].UpData();

        //デバック用

        if (Input.GetKeyDown(KeyCode.Y)) CreateErrorBuff();
        if (Input.GetKeyDown(KeyCode.H)) Clear();


    }

    public void CreateErrorBuff()
    {
        Errorbuff errorbuff = new MouseJammer();

        _errorList.Add(errorbuff);

        for (int i = 0; i < _errorList.Count; i++)
            _errorList[i].Start();

    }
    public void Clear() 
    {
        _errorList.Clear();
    }
    private class Errorbuff
    {
        public virtual void Start()
        {
        }
        public virtual void UpData()
        {
        }
        /// <summary>
        /// バフの個数をカウントする関数
        /// </summary>
        public System.Func<int> errorCount;
    }


    /// <summary>
    /// マウスジャマーのインナークラス
    /// </summary>
    private class MouseJammer : Errorbuff
    {
        private float _renge = 0;
        private Vector2 _lostPos = Vector2.zero;

        /// <summary>
        /// 今は決め打ち
        /// </summary>
        private float MaxRenge = 30;

        public override void Start()
        {
            POINT pOINT = new POINT();
            GetCursorPos(out pOINT);


            _lostPos = new Vector2(pOINT.X, pOINT.Y);
            _renge = 0;

            errorCount = () =>
            {

                int count = 0;

                List<Card.Trump> trumps = CardManager.instance.GetDeck();


                for (int i = 0; i < trumps.Count; i++)
                {
                    if (trumps[i].cardBuff != Card.cardBuff.MouseJammer) continue;
                    count++;
                }


                //ジョーカーの分もカウント
                JokerUtility.JokerALLAction(joker =>
                {

                    if (joker.GetCardBuff() != Card.cardBuff.MouseJammer) return;
                    count++;

                });


                //デバックの為に個数を３で固定する

                //count = 3;


                return count;

            };



        }

        public override void UpData()
        {

            if (errorCount() == 0) return;


            POINT pOINT = new POINT();
            GetCursorPos(out pOINT);

            _renge += Vector2.Distance(_lostPos, new Vector2(pOINT.X, pOINT.Y));
            _lostPos = new Vector2(pOINT.X, pOINT.Y);


            if (MaxRenge > _renge) return;
            MouseMove();


        }

        private void MouseMove()
        {

            float renge = 100;//UnityEngine.Random.Range(1,5*errorCount());

            float randomAngle = UnityEngine.Random.Range(1, 360) * Mathf.Deg2Rad;

            SetCursorPos((int)(Mathf.Sin(randomAngle) * renge + _lostPos.x), (int)(Mathf.Cos(randomAngle) * renge + _lostPos.y));

            Start();


        }





    }

}


