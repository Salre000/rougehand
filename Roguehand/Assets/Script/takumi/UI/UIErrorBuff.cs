using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static ErrorBuffDetalis;
public class UIErrorBuff : MonoBehaviour
{
    [SerializeField]UICardManager _managers;

    [SerializeField] systemBuff _thisBuff= systemBuff.None;



    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _explantion;
    [SerializeField] TextMeshProUGUI _lavel;

    private readonly int MOUSE_ID=IDUtility.BUFF_ID+103;
    private readonly int BRACK_ID=IDUtility.BUFF_ID+5;
    private readonly int OBJECT_MOVE_ID=IDUtility.BUFF_ID+302;
    private readonly int DLIND_SCORE_ID=IDUtility.BUFF_ID+208;
    private readonly int EXPLANTION_ID = 50;

    public void SetCard(List<GameObject> objects) 
    {
        List<MeshRenderer> meshRenderers = new();

        for (int i = 0; i < objects.Count; i++) 
        {
            objects[i].transform.parent = _managers.gameObject.transform;

            // ƒ}ƒeƒŠƒAƒ‹‚ð‚Í‚é
            switch (_thisBuff)
            {
                case systemBuff.Mouse:
                    break;
                case systemBuff.Brack:
                    break;
                case systemBuff.ObujectMove:
                    break;
                case systemBuff.Number:
                    break;
            }
        }
        _managers.Show();

        _name.text = GetName();

        _explantion.text = GetEXPLANTION();

    }


    private string GetEXPLANTION() 
    {
        string name=string.Empty;
        switch (_thisBuff)
        {
            case systemBuff.Mouse:
                name = MasterData.instance.GetStringMaster(MOUSE_ID+ EXPLANTION_ID);
                break;
            case systemBuff.Brack:
                name = MasterData.instance.GetStringMaster(BRACK_ID + EXPLANTION_ID);

                break;
            case systemBuff.ObujectMove:
                name = MasterData.instance.GetStringMaster(OBJECT_MOVE_ID + EXPLANTION_ID);

                break;
            case systemBuff.Number:

                name = MasterData.instance.GetStringMaster(DLIND_SCORE_ID + EXPLANTION_ID);
                break;
        }
        return name;
    }
    private string GetName() 
    {
        string name=string.Empty;

        switch (_thisBuff)
        {
            case systemBuff.Mouse:
                name = MasterData.instance.GetStringMaster(MOUSE_ID);
                break;
            case systemBuff.Brack:
                name = MasterData.instance.GetStringMaster(BRACK_ID);

                break;
            case systemBuff.ObujectMove:
                name = MasterData.instance.GetStringMaster(OBJECT_MOVE_ID);

                break;
            case systemBuff.Number:

                name = MasterData.instance.GetStringMaster(DLIND_SCORE_ID);
                break;
        }

        name = Extra.ErrorText(name);

        return name;
    }



}
