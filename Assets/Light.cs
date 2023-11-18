using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    [SerializeField] private int light;

    public static Light Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

    }

    private void Start()
    {
        light = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Light")
        {
            light++;
            Destroy(other.gameObject);
        }

    }

    public int GetLight()
    {
        return light;
    }
}
