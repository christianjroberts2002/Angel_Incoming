using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    private Vector3 sineWave;
    [SerializeField] private float x;
    [SerializeField] private float xTwo;

    [SerializeField] float randomOffset;
    [SerializeField] private float randomStartingHeight;

    [SerializeField] private float offsetMax;
    [SerializeField] private float offsetMin;

    [SerializeField] private float heightMax;
    [SerializeField] private float heightMin;

    [SerializeField] private bool onCloud;

    [SerializeField] private float test;

    [SerializeField] private float hitForce;
    [SerializeField] private float originalHitForce;
    // Start is called before the first frame update
    void Start()
    {
        randomOffset = Random.Range(offsetMin, offsetMax);
        randomStartingHeight = Random.Range(heightMin, heightMax);
        test = 1000f;
    }

    // Update is called once per frame
    void Update()
    {
        
        x += Time.deltaTime;
        xTwo -= Time.deltaTime;
        sineWave = new Vector3(transform.position.x, (Mathf.Sin(x + randomOffset) + randomStartingHeight) /*+ ((Mathf.Sin(x + randomOffset)) * Mathf.PingPong(Time.time, hitForce) * Mathf.Sin(x))*/, transform.position.z);
        transform.position = sineWave;

        //if (hitForce < -.01f)
        //{
        //    hitForce += .002f * Time.deltaTime;
        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(PlayerController.Instance.IsGrounded() && !onCloud)
        {

            hitForce = originalHitForce;
            
            onCloud = true;

        }


    }

    private void OnTriggerExit(Collider other)
    {
        onCloud = false;
    }

    public float GetDownwardVelocity(Collision other)
    {
        return other.relativeVelocity.magnitude;
    }

}
