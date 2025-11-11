using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICardObject : MonoBehaviour
{
    [SerializeField] Image _main;
    [SerializeField] Image _effect;
    [SerializeField] Image _seal;
    [SerializeField] Image _color;

    readonly Color INITIALIZ_COLOR = new Color(0, 0, 0, 0);

    private Color _nowColor;

    public void SetImage(Material main, Material effect, Material seal)
    {
        _main.material = main;
        if (effect != null) { _effect.material = effect; _effect.color = Color.white; };
        if (seal != null){ _seal.material = seal; _seal.color = Color.white; };
        

        _color.color = _nowColor;

    }

    public void ResetImage() 
    {
        _nowColor = INITIALIZ_COLOR;
        _effect.color = INITIALIZ_COLOR;
        _seal.color = INITIALIZ_COLOR;
        _color.color = INITIALIZ_COLOR;


    }

    public void SetNowColor(Color color) { _nowColor = color; }

}
