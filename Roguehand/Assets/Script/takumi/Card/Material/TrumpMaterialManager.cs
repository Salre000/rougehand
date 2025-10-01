using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrumpMaterialManager : MonoBehaviour
{
    [SerializeField]private List<List<Material>>_cardMaterial=new List<List<Material>> ();

    private Material _baseMaterial;
    public void Initializ() 
    {
        _baseMaterial = Resources.Load<Material>("takumi/BaseMaterial");

        Sprite[][] sprites=new Sprite[4][];

        MaterialList materialList= Resources.Load<MaterialList>("takumi/TrumpImage/MaterialObject");


        List<List<Texture>> material= new List<List<Texture>> ();
        material.Add(materialList._materialS);
        material.Add(materialList._materialH);
        material.Add(materialList._materialD);
        material.Add(materialList._materialC);
        for (int i = 0; i < 4; i++) 
        {
            _cardMaterial.Add(new List<Material>());
            for (int j = 0; j < 13; j++) 
            {

                Material materialCopy = new Material(_baseMaterial);

                materialCopy.SetTexture("_MainTex", material[i][j]);

                _cardMaterial[i].Add(materialCopy);
            }



        }

    }


    public Material GetMaterial(int suit,int number) { Debug.Log(suit + ":" + number); return _cardMaterial[suit][number-1];}

    
    

}
