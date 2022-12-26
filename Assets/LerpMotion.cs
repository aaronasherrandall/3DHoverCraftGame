using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpMotion : MonoBehaviour
{

    public Transform hoverCraft;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, hoverCraft.transform.position, Time.deltaTime * speed);
        
    }
}
