using System;
using UnityEngine;

public class ArrowObject : MonoBehaviour
{
    [SerializeField] private float lifetime = 2f;

    public float speed;
    public float damage;

    private Transform tr;

    void Awake()
    {
        tr = transform;
    }

    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), lifetime);
    }

    // Update is called once per frame
    void Update()
    {
        tr.position += tr.forward * speed * Time.deltaTime;
        
    }
    

    private void ReturnToPool()
    {
        ObjectPoolManager.Instance.ReturnObject(gameObject);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
}
