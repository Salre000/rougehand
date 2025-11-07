using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleObjectManager : DetailsBase
{
    private GameObject _pollParent;
    [SerializeField] List<RoleObject> _roleObjectPool = new List<RoleObject>();


    private readonly float _MAX_COUNT = (int)RoleManager.Role.max;

    [SerializeField] private GameObject prefab;
    [SerializeField] private GameObject _scrollObject;

    public override void Show()
    {
        for(int i = 0; i < _MAX_COUNT; i++) 
        {
            RoleObject obj = GetRoleObject();

            obj.Show((RoleManager.Role)i);


        }
        
    }
    public override void Hide()
    {
        for (int i = 0; i < _MAX_COUNT; i++)
        {
            RoleObject obj = _roleObjectPool[i];

            obj.gameObject.SetActive(false);

            obj.transform.SetParent(_pollParent.transform);

        }

    }
    public override void Initializ()
    {
        _pollParent = new GameObject("RoleObjectPool");


        for (int i = 0; i < _MAX_COUNT; i++)
        {
            GameObject roleObject = Instantiate(prefab, _pollParent.transform);

            _roleObjectPool.Add(roleObject.GetComponent<RoleObject>());

            roleObject.SetActive(false);




        }

    }

    private RoleObject GetRoleObject()
    {
        for (int i = 0; i < _roleObjectPool.Count; i++)
        {
            RoleObject roleObject = _roleObjectPool[i];
            if (roleObject.gameObject.activeSelf) continue;

            roleObject.gameObject.SetActive(true);

            roleObject.transform.SetParent(_scrollObject.transform);

            return roleObject;

        }

        return null;
    }



}
