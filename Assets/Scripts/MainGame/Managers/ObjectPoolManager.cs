using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager _instance;

    public static ObjectPoolManager Instance { get { return _instance; } }

    [Header("Poolable Objects")]
    public GameObject[] prefabReferences;
    [SerializeField] private int defaultPoolSize;

    private Dictionary<GameObject, Queue<GameObject>> _pooledInstances; // reference key, list of instances

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    private void Start()
    {
        InstantiatePools();
    }

    private void InstantiatePools()
    {
        _pooledInstances = new Dictionary<GameObject, Queue<GameObject>>();
        foreach (GameObject reference in prefabReferences)
        {
            _pooledInstances[reference] = new Queue<GameObject>();
            CreatePool(reference);
        }
    }

    public void CreatePool(GameObject reference)
    {
        if (_pooledInstances.ContainsKey(reference))
            return;
        for (int i = 0; i < defaultPoolSize; i++)
        {
            _pooledInstances[reference].Enqueue(Instantiate(reference));
            _pooledInstances[reference].Peek().SetActive(false);
        }
    }

    public GameObject GetObject(GameObject reference)
    {
        if (_pooledInstances.ContainsKey(reference))
            CreatePool(reference);
        GameObject returnObj;
        if(_pooledInstances[reference].Count > 0)
            returnObj = _pooledInstances[reference].Dequeue();
        else
            returnObj = Instantiate(reference);
        returnObj.SetActive(true);
        return returnObj;
    }

    public void ReturnObject(GameObject reference)
    {
        if (_pooledInstances.ContainsKey(reference))
            CreatePool(reference);
        reference.SetActive(false);
        _pooledInstances[reference].Enqueue(reference);
    }
}