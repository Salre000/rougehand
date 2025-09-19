using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerBase
{
    /// <summary>
    /// ���E���h�̊J�n���̃W���[�J�[�̋���
    /// </summary>
    public virtual void RoundStart() { }

    /// <summary>
    /// ��ɉ񂷃W���[�J�[�̋����i��{�I�ɒ����Ƀ��^�[���ŕԂ��֐��j
    /// </summary>
    public virtual void UpData() { }

    /// <summary>
    /// �W���[�J�[�̃^�[��������ė������ɓ�������
    /// </summary>
    /// <returns><��{�[�������ǂ��ꂪ�{��������/returns>
    public virtual int Trun() {  return 0; }

    /// <summary>
    /// ���E���h�̏I�����̃W���[�J�[�̋���
    /// </summary>
    public virtual void RoundEnd() { }


}
