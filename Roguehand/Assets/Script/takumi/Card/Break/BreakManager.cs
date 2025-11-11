using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakManager : MonoBehaviour
{

    /// <summary>
    /// カードの破壊時に使うプレハブ
    /// </summary>
    [SerializeField] GameObject _breakCardPrefab;

    /// <summary>
    /// カードの破壊に使うオブジェクトのプール
    /// </summary>
    private List<GameObject> _gameObjects = new List<GameObject>();

    private Vector3 _offset = new Vector3(0,0,-15f);

    private readonly Vector3 POS = new Vector3(90, 0, 0);

    public void Awake()
    {
        BreakUtility.instance = this;
        for (int i = 0; i < 10; i++)
        { 
            _gameObjects.Add(Instantiate(_breakCardPrefab, transform));

            _gameObjects[_gameObjects.Count-1].SetActive(false);
        }
    }



    /// <summary>
    /// カードの破壊を行う関数
    /// </summary>
    /// <param name="gameObject"></param>
    public void StartBreak(GameObject gameObject) 
    {
        GameObject breakObject= GetObject();

        breakObject.transform.position = gameObject.transform.position+_offset;

        breakObject.transform.parent = gameObject.transform;

        breakObject.transform.localEulerAngles = POS;

        breakObject.transform.parent = transform;



    }

    private GameObject GetObject() 
    {

        for(int i = 0; i < _gameObjects.Count; i++) 
        {
            if (_gameObjects[i].activeSelf) continue;
            _gameObjects[i].SetActive(true);

            return _gameObjects[i];


        }
        return null;

    }


}
