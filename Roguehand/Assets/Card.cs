public class Card
{
    public enum suit
    {
        None = -1,
        Spade,
        heart,
        diamond,
        club
    }

    public enum number
    {
        None=-1,
        two = 2, 
        three,
        four,
        five,
        six,
        seven,
        eight,
        nine,
        ten,
        jack,
        queen, 
        king,
        ace = 1
    }

    /// <summary>
    /// ���̃o�t�Ƌ������Ȃ��o�t��type
    /// </summary>
    public enum sealBuff
    {
        None = -1
    }
    /// <summary>
    /// ������J�[�h�Ƀo�t���\��type
    /// </summary>
    public enum cardBuff
    {
        None = -1
    }
    /// <summary>
    /// �f�b�L�̃J�[�h�Ƀo�t���\��type
    /// </summary>
    public enum deckBuff
    {
        None = -1
    }
    /// <summary>
    /// �W���[�J�[�݂̂Ƀo�t���\��type
    /// </summary>
    public enum JokerBuff
    {
        None = -1,

    }
    /// <summary>
    /// �ꖇ�̃J�[�h�̏��
    /// </summary>
    public struct Trump
    {
        suit _suit;
        number _number;
        sealBuff _sealBuff;
        cardBuff _cardBuff;
        deckBuff _deckBuff;
    }


}
