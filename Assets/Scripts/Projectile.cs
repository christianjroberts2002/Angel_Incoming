using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;

    // Update is called once per frame
    void Update()
    {
        MoveProjectile();
    }

    private void MoveProjectile()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime, Space.Self);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Ground")
        {
            Destroy(gameObject);

        }
    }
}
