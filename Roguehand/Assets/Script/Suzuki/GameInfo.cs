using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo 
{
    // メインゲーム左側の情報集合体にに必要な情報たち
    public struct Info
    {
        public string roundname;    // 現ラウンドの名前
        public int rowestscore;     // 現在の最低目標スコア
        public int reward;          // 突破時にもらえる金額
        public int roundscore;      // 現ラウンドの合計スコア
        public int had;             // 残りハンド回数
        public int discard;         // 残りディスカード回数
        public string role;         // プレイされた役
        public int rolelevel;       // 役のレベル
        public int basicscore;      // スコア
        public int magnification;   // 倍率
        public int money;           // 所持金
        public int round;           // ラウンド
        public int ante;            // アンティ
    } 
}
