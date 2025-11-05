using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICardManager : MonoBehaviour
{
    [SerializeField, Header("並べるスート")] Card.suit suit = Card.suit.Spade;

    private readonly float WIDE_SIZE = 810;

    private readonly float MAX_ANGLE = 30f;
    private readonly float MAX_HEIGHT = 100f;
    private readonly float OFFSET = 25f;

    private readonly float CARD_POS_Z = -20;
    public void Show()
    {

        // 個構造の数を取得
        int count = transform.childCount;

        float renge = WIDE_SIZE / (count + 1);

        for (int i = 0; i < count; i++)
        {
            RectTransform rectTransform = transform.GetChild(i).GetComponent<RectTransform>();

            Vector3 pos = Vector3.zero;


            float angleRange = renge * (i + 1) / WIDE_SIZE;

            angleRange -= 0.5f;
            angleRange *= -2;

            pos.x = renge * (i + 1)- WIDE_SIZE / 2;
            pos.y = -(Mathf.Abs(angleRange) * MAX_HEIGHT)+ OFFSET;
            pos.z = CARD_POS_Z;

            rectTransform.localPosition = pos;
            Vector3 angle = rectTransform.eulerAngles;

            angle.z = angleRange * MAX_ANGLE;
            rectTransform.eulerAngles = angle;


        }






    }
}
