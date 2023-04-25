using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    public  float smoothing =5;
    Vector3 offset;
    
    void Start()
    {
        offset = transform.position - player.position ;
    }
     void FixedUpdate()
    {
        Vector3 MousePos = player.position + offset;
        transform.position = Vector3.Lerp(transform.position,MousePos,smoothing * Time.deltaTime);
    }

   
}
