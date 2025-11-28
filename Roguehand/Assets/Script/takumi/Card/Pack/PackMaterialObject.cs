using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackMaterialObject : MonoBehaviour
{
    [SerializeField] MeshRenderer _main;
    [SerializeField] MeshRenderer _upObject;

    private readonly int MAIN_MATERIAL_INDEX = 1;
    private readonly int SBU_MATERIAL_INDEX = 0;

    /// <summary>
    /// オブジェクトのマテリアルを貼り付ける関数
    /// </summary>
    /// <param name="main"><見出しのマテリアル/param>
    /// <param name="sbu"><表面以外/param>
    public void SetMaterial(Material main,Material sbu) 
    {

        Material[] materials = _main.materials;
        materials[MAIN_MATERIAL_INDEX] = main;
        materials[SBU_MATERIAL_INDEX] =sbu ;
        _main.materials = materials;

        _upObject.material = sbu;


    }
}
