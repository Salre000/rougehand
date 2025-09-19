using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public static class UIManager
{

    private static TextMeshProUGUI _roundNameText;
    private static TextMeshProUGUI _lowestScereText;
    private static TextMeshProUGUI _rewardCountText;
    private static TextMeshProUGUI _roundScereText;
    private static TextMeshProUGUI _roleText;
    private static TextMeshProUGUI _basicScoreText;
    private static TextMeshProUGUI _magnificationText;
    private static TextMeshProUGUI _handText;
    private static TextMeshProUGUI _discardText;
    private static TextMeshProUGUI _moneyText;
    private static TextMeshProUGUI _anteText;
    private static TextMeshProUGUI _roundText;


    public struct UITexts
    {
        public string roundname;        // ���E���h�̖��O
        public string lowestscore;      // �Œ�X�R�A
        public string rewardCount;      // ��V��
        public string roundscore;       // ���E���h���œ������v�X�R�A
        public string role;             // ���̖��O�ƃ��x��
        public string basicscore;       // ��b�_
        public string magnification;    // �{��
        public string hand;             // �n���h�̉�
        public string discard;          // �f�B�X�J�[�h�̉�
        public string money;            // ������
        public string ante;             // ��Փx
        public string round;            // �N���A�������E���h��
    }

    public static void Initialize()
    {
        UITexts texts = new UITexts();
        texts.roundname = "";
        texts.lowestscore = "";
        texts.rewardCount = "";
        texts.roundscore = "0";
        texts.role = "";
        texts.basicscore = "0";
        texts.magnification = "0";
        texts.hand = "5";
        texts.discard = "5";
        texts.money = "$0";
        texts.ante = "0/8";
        texts.round = "0";

        #region �e�L�X�g�̎擾

        _roundNameText = GameObject.Find("RoundNameText").GetComponent<TextMeshProUGUI>();
        _lowestScereText = GameObject.Find("LowestScoreText").GetComponent<TextMeshProUGUI>();
        _rewardCountText = GameObject.Find("RewardCountText").GetComponent<TextMeshProUGUI>();
        _roundScereText = GameObject.Find("RoundScoreText").GetComponent<TextMeshProUGUI>();
        _roleText = GameObject.Find("RoleText").GetComponent<TextMeshProUGUI>();
        _basicScoreText = GameObject.Find("BasicScoreText").GetComponent<TextMeshProUGUI>();
        _magnificationText = GameObject.Find("MagnificationText").GetComponent<TextMeshProUGUI>();
        _handText = GameObject.Find("HandText").GetComponent<TextMeshProUGUI>();
        _discardText = GameObject.Find("DisCardText").GetComponent<TextMeshProUGUI>();
        _moneyText = GameObject.Find("MoneyText").GetComponent<TextMeshProUGUI>();
        _anteText = GameObject.Find("AnteText").GetComponent<TextMeshProUGUI>();
        _roundText = GameObject.Find("RoundText").GetComponent<TextMeshProUGUI>();

        #endregion

        #region �e�L�X�g�̏�����
        SetRoundNameText(texts.roundname);
        SetLowestScoreText(texts.lowestscore);
        SetRewardCountText(texts.rewardCount);
        SetRoundScereText(texts.roundscore);
        SetRoleText(texts.role);
        SetBasicScoreText(texts.basicscore);
        SetMagnificationText(texts.magnification);
        SetHandText(texts.hand);
        SetDiscardText(texts.discard);
        SetMoneyText(texts.money);
        SetAnteText(texts.ante);
        SetRoundText(texts.round);
        #endregion
    }

    #region �Q�b�^�[

    public static string GetRoundNameText() { return _roundNameText.text; }
    public static string GetLowestScoreText() { return _lowestScereText.text; }
    public static string GetRewardText() { return _rewardCountText.text; }
    public static string GetRoundScoreText() { return _roundScereText.text; }
    public static string GetRoleText() { return _roleText.text; }
    public static string GetBasicScoreText() { return _basicScoreText.text; }
    public static string GetMagnificationText() { return _magnificationText.text; }
    public static string GetHandText() { return _handText.text; }
    public static string GetDiscardText() { return _discardText.text; }
    public static string GetMoneyText() { return _moneyText.text; }
    public static string GetAnteText() { return _anteText.text; }
    public static string GetRoundText() { return _roundText.text; }

    #endregion

    #region �Z�b�^�[

    public static void SetRoundNameText(string value) { _roundNameText.text= value; }
    public static void SetLowestScoreText(string value) { _lowestScereText.text= value; }
    public static void SetRewardCountText(string value) { _rewardCountText.text = value; }
    public static void SetRoundScereText(string value) { _roundScereText.text = value; }
    public static void SetRoleText(string value) { _roleText.text= value; }
    public static void SetBasicScoreText(string value) { _basicScoreText.text = value; }
    public static void SetMagnificationText(string value) { _magnificationText.text = value; }
    public static void SetHandText(string value) { _handText.text = value; }
    public static void SetDiscardText(string value) { _discardText.text = value; }
    public static void SetMoneyText(string value) { _moneyText.text = value; }
    public static void SetAnteText(string value) { _anteText.text = value; }
    public static void SetRoundText(string value) { _roundText.text = value; }

    #endregion

    /// <summary>
    /// ������𐮐��ɕϊ��A���������Ȃ�out�ɏo��
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int IntTryParse(string value) 
    {
        int result = 0;
        if(int.TryParse(value, out result))
        {
            return result;
        }
        Debug.Log("!!!FAILED!!!");
        return result=-1;
    }
}
