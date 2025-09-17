using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInfo 
{
    // ���C���Q�[�������̏��W���̂ɂɕK�v�ȏ�񂽂�
    public struct Info
    {
        public string roundname;    // �����E���h�̖��O
        public int rowestscore;     // ���݂̍Œ�ڕW�X�R�A
        public int reward;          // �˔j���ɂ��炦����z
        public int roundscore;      // �����E���h�̍��v�X�R�A
        public int had;             // �c��n���h��
        public int discard;         // �c��f�B�X�J�[�h��
        public string role;         // �v���C���ꂽ��
        public int rolelevel;       // ���̃��x��
        public int basicscore;      // �X�R�A
        public int magnification;   // �{��
        public int money;           // ������
        public int round;           // ���E���h
        public int ante;            // �A���e�B
    } 
}
