using UnityEngine;

public class ArrowObject : MonoBehaviour
{
    public float speed;
    public float damage;

    private Transform tr;

    void Awake()
    {
        tr = transform;
    }

    // Update is called once per frame
    void Update()
    {
        tr.position += tr.forward * speed * Time.deltaTime;
    }
}
