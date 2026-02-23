using System;
using UnityEngine;

public class ArrowHazard : MonoBehaviour
{
    public GameObject arrowPrefab;
    [SerializeField] float shootInterval;
    private float shootIntervalLeft;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        shootIntervalLeft = shootInterval;
    }

    // Update is called once per frame
    void Update()
    {
        shootIntervalLeft -= Time.deltaTime;
        if (shootIntervalLeft <= 0)
        {
            Quaternion rotation = Quaternion.Euler(0f, 180f, 0f);

            GameObject arrowGO = ObjectPoolManager.Instance.GetObject(arrowPrefab);

            arrowGO.transform.position = transform.position;
            arrowGO.transform.rotation = rotation;

            shootIntervalLeft = shootInterval;
        }
    }
}
