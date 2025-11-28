using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackMaterialManager : MonoBehaviour
{
    [SerializeField] Material _silver;

    MaterialstringList _packMaterialList;

    private readonly string _PACK_MATERIAL_FILE_NAME = "takumi/PackMaterialList";


    private enum packSizeType 
    {
        None=-1,
        normal,
        mega,
        max
    }

    private Material[][] materialArray=new Material[(int)packSizeType.max][];



    private void Awake()
    {
        _packMaterialList = Resources.Load<MaterialstringList>(_PACK_MATERIAL_FILE_NAME);

        // パックのマテリアルを予め生成
        for(int i = 0; i < (int)packSizeType.max; i++) 
        {
            materialArray[i]=new Material[(int)InstantiatePack.PackType.max];

            for(int j=0;j< (int)InstantiatePack.PackType.max; j++) 
            {
                // シルバーを原型にコピーを作成
                Material dommy = new Material(_silver);
                dommy.SetTexture("_MainTex", _packMaterialList._material[(i*((int)InstantiatePack.PackType.max)+j)]);
                
                // 配列に追加していく
                materialArray[i][j] = dommy;
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="packObject"><パックのオブジェクト自体/param>
    /// <param name="type"><パックの種類/param>
    /// <param name="packCount"><パックの大きさ/param>
    public void SetPackPaint(GameObject packObject,InstantiatePack.PackType type,int packCount) 
    {
        PackMaterialObject pack=packObject.GetComponent<PackMaterialObject>();

        pack.SetMaterial(GetMaterial(type,packCount), _silver);



    }

    private Material GetMaterial(InstantiatePack.PackType type,int packCount) 
    {
        return materialArray[PackSize(packCount)][(int)type];
    }

    private int PackSize(int packSize) 
    {
        if (packSize < 5) return (int)packSizeType.normal;

        return (int)packSizeType.mega;



    }

}
