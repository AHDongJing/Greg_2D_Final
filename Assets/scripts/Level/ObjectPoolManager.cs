using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager
{

    private Dictionary<string, List<GameObject>> dic;

    private static ObjectPoolManager instance;

    public static ObjectPoolManager Instance
    {
        get {
            if (instance == null)
                instance = new ObjectPoolManager();
            return instance;
        }
    }
    private ObjectPoolManager()
    {
        dic = new Dictionary<string, List<GameObject>>();
    }

    public void Set(string key,GameObject obj)
    {
        if (!dic.ContainsKey(key))
        {
            dic[key] = new List<GameObject>();
            //dic.Add(key, new List<GameObject>());
        }
        obj.SetActive(false);
        dic[key].Add(obj);
    }

    public GameObject Get(string key,GameObject prefab,Vector3 position,Quaternion rotation)
    {
        GameObject obj = null;
        if (dic.ContainsKey(key))
        {
            if (dic[key].Count != 0)
            {
                obj = dic[key][0];
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                obj.SetActive(true);
                dic[key].RemoveAt(0);
                return obj;
            }
        }
       // GameObject prefab = Resources.Load<GameObject>(prefabPath);
        obj = GameObject.Instantiate(prefab, position, rotation);
        return obj;
    }

    public void Clear()
    {
        instance = null;
    }
}
