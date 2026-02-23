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

    private Dictionary<GameObject, Queue<GameObject>> _pooledInstances;
    private Dictionary<GameObject, GameObject> _instanceToReference; // instance -> prefab


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
        _instanceToReference = new Dictionary<GameObject, GameObject>();
        foreach (GameObject reference in prefabReferences)
            CreatePool(reference);
    }

    public void CreatePool(GameObject reference)
    {
        if (_pooledInstances.ContainsKey(reference))
            return;
        _pooledInstances[reference] = new Queue<GameObject>();
        for (int i = 0; i < defaultPoolSize; i++)
        {
            GameObject obj = Instantiate(reference);
            obj.SetActive(false);
            _instanceToReference[obj] = reference;
            _pooledInstances[reference].Enqueue(obj);
        }
    }

    public GameObject GetObject(GameObject reference)
    {
        if (!_pooledInstances.ContainsKey(reference))
            CreatePool(reference);
        GameObject returnObj = _pooledInstances[reference].Count > 0
            ? _pooledInstances[reference].Dequeue()
            : Instantiate(reference);
        _instanceToReference[returnObj] = reference;
        returnObj.SetActive(true);
        return returnObj;
    }

    public void ReturnObject(GameObject instance)
    {
        if (!_instanceToReference.ContainsKey(instance))
        {
            Debug.LogWarning("Returned object not tracked: " + instance.name);
            return;
        }
        GameObject reference = _instanceToReference[instance];
        instance.SetActive(false);
        _pooledInstances[reference].Enqueue(instance);
    }
}