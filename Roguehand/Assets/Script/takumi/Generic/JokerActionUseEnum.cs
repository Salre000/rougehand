using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �W���[�J�[�̃A�N�V�����Ɏg���񋓑̂��܂Ƃ߂��N���X
/// </summary>
public static class JokerActionUseEnum 
{
    //�������������̉����̗񋓑�
    public enum JokerActionTarget
    {
        /// <summary>
        /// �����J�[�h�̎g�p��
        /// </summary>
        constellation,
        /// <summary>
        /// �A�C�e���̎g�p��
        /// </summary>
        item,
        /// <summary>
        /// ��������̔��p��
        /// </summary>
        sale,
        /// <summary>
        /// �n���h���������߂��Ƃ�
        /// </summary>
        role,

        max

    }

    public static readonly string[] JokerActionTargetExplanation = new string[(int)JokerActionTarget.max+1]
    {
        "�����J�[�h�̎g�p��",
        "�A�C�e���̎g�p��",
        "���p�̎g�p��",
        "���̎g�p��",
        "����"

    };

    /// <summary>
    /// �{�����ǂ̂悤�ɒǉ����邩
    /// </summary>
    public enum AddType 
    {
        /// <summary>
        /// ���Z
        /// </summary>
        addition,

        /// <summary>
        /// ��Z
        /// </summary>
        Multiplication,
        max

    }

    /// <summary>
    /// �ǂ̃^�C�~���O�ŉ��Z�����邩
    /// </summary>
    public enum Timing 
    {
        /// <summary>
        /// �W���[�J�[�̃^�[���ɉ��Z
        /// </summary>
        trun,

        /// <summary>
        /// �������������ꂽ�u��
        /// </summary>
        now,

        /// <summary>
        /// ���ジ���ƃW���[�J�[�̔{���������Ȃ�
        /// </summary>
        never,

        max

    }
    public static readonly string[] JokerActionTimingExplanation = new string[(int)Timing.max + 1]
{
        "�W���[�J�[�̃^�[����",
        "�������������ꂽ�u��",
        "���ジ����",
        "����"

};




}
