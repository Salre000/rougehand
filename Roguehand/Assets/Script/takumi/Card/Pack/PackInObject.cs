using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PackInObject : MonoBehaviour
{

    private readonly Vector2 ANGLE = new Vector2(180f, 0);

    private Vector3 _tragetPos;

    private Vector3 _startPos;


    private float time = 0;


    public void Awake()
    {
        _startPos = transform.position;

    }
    public void Update()
    {
        if (time > 1) return;

        time += Time.deltaTime;

        transform.position = Vector3.Lerp(_startPos, _tragetPos, time);
        transform.eulerAngles = Vector3.Lerp(Vector3.zero, ANGLE, time);

    }

    public void SetTragetPos(Vector3 vector) { _tragetPos = vector; }

}
