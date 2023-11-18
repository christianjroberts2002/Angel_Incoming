using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{
    public static MousePosition Instance;

    [SerializeField] Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Vector3 mouseWorldPos;
    private Vector3 currentPos;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        currentPos = transform.position;

    }
    private void Update()
    {
        mouseWorldPos = MouseToWorld();
        transform.position = mouseWorldPos;
    }

    private Vector3 MouseToWorld()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            currentPos = raycastHit.point;

        }
        return currentPos;



    }

    public Vector3 GetMousePosition()
    {
        return mouseWorldPos;
    }

    
}
