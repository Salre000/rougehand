using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeColl : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ShakeCamera.Instance.Shake(5, 0.2f);
        }
    }
}