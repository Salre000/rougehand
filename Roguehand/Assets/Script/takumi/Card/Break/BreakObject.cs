using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakObject : MonoBehaviour
{
    /// <summary>
    /// このオブジェクトのアニメーション
    /// </summary>
    private Animator _animator;

    /// <summary>
    /// このオブジェクトの時間経過
    /// </summary>
    private float _time = 0;

    /// <summary>
    /// このオブジェクトのマテリアル
    /// </summary>
    [SerializeField] private Material _materialBase;

    /// <summary>
    /// このオブジェクトのマテリアル
    /// </summary>
    private Material _material;

    /// <summary>
    /// 最大の時間
    /// </summary>
    private readonly float MAX_TIME = 5; 

    public void Awake()
    {
        _animator = GetComponent<Animator>();

        _material=new Material( _materialBase );

        for(int i = 0; i < transform.childCount; i++) 
        {

            MeshRenderer meshRenderer=transform.GetChild(i).GetComponent<MeshRenderer>();

            meshRenderer.material = _material;

        }


    }
    public void OnEnable()
    {
        Color color = _material.color;

        color.a = 1;

        _material.color = color;

        _animator.SetTrigger("Break");

    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        LowAlpha();
        if (_time < MAX_TIME) return;
        _time = 0;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// アルファ値を変更する関数
    /// </summary>
    private void LowAlpha() 
    {
        Color color= _material.color;

        color.a = 1f-(_time / MAX_TIME);

        _material.color = color;

    }
}
