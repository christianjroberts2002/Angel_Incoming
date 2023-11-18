using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudColor : MonoBehaviour
{
    [SerializeField] Gradient cloudGradient;
    [SerializeField] Color cloudColor;
    [SerializeField] Material cloudMaterial;
    private MeshRenderer meshRenderer;
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        cloudColor = cloudGradient.Evaluate(Random.Range(0.0f, 1.0f));
        meshRenderer.material.color = cloudColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
