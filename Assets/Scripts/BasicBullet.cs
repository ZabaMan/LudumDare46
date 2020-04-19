using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicBullet : MonoBehaviour
{
    public float destroyAfter;

    public float speed;

    public bool destroyOnHit = true;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyAfter);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(destroyOnHit)
        Destroy(gameObject);
    }
}
