using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] Transform arrowPrefab;
    [SerializeField] Transform bow;

    [SerializeField] float destroyTime;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShootArrow();
    }

    private void ShootArrow()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Transform arrow = Instantiate(arrowPrefab, bow.transform.position , transform.rotation);
            Destroy(arrow.gameObject, destroyTime);
        }
    }

}
