using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ScriptCountNumber;
public class UICardManager : MonoBehaviour
{
    [SerializeField, Header("並べるスート")] Card.suit suit = Card.suit.Spade;

    private readonly float WIDE_SIZE = 910;

    private  float MAX_HEIGHT = 20f;
    private readonly float OFFSET = 15;

    private readonly float CARD_POS_Z = -20;

    float centerPointY = 4000f;

    private readonly float _HEIGHT_OFFSET = 0.5f;
    public void Show()
    {
        RectTransform thisRect=GetComponent<RectTransform>();

        // 個構造の数を取得
        int count = transform.childCount;

        float renge = WIDE_SIZE / (count + 1);

        for (int i = 0; i < count; i++)
        {
            RectTransform rectTransform = transform.GetChild(i).GetComponent<RectTransform>();

            Vector3 pos = Vector3.zero;


            float angleRange = renge * (i + 1) / WIDE_SIZE;

            angleRange -= _HEIGHT_OFFSET;
            angleRange *= DOUBLE;

            pos.x = renge * (i + 1)- WIDE_SIZE / HALF;
            pos.y = -Mathf.Abs( Mathf.Sin(angleRange))* MAX_HEIGHT+OFFSET;
            pos.z = CARD_POS_Z;

            rectTransform.localPosition = pos;
            Vector3 angle = rectTransform.eulerAngles;

            Vector2 vec= rectTransform.localPosition-new Vector3(0, centerPointY, 0);


            angle.z =HALF_ROTATION-( Mathf.Atan2(vec.x,vec.y)*Mathf.Rad2Deg)*ALTERNATIVE;
            rectTransform.eulerAngles = angle;


        }






    }
}
