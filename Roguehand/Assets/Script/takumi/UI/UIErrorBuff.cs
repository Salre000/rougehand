using System.Collections.Generic;
using UnityEngine;
using static ErrorBuffDetalis;
public class UIErrorBuff : MonoBehaviour
{
    [SerializeField]UICardManager _managers;

    [SerializeField] systemBuff _thisBuff= systemBuff.None;


    public void SetCard(List<GameObject> objects) 
    {
        List<MeshRenderer> meshRenderers = new();


        for (int i = 0; i < objects.Count; i++) 
        {
            objects[i].transform.parent = _managers.gameObject.transform;

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

    }



}
