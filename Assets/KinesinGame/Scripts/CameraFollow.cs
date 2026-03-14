using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;
    Vector3 offset;
    void Start()
    {
     offset= transform.position - player.position;   
    }

   
    void Update()
    {
        Vector3 targetPosition = player.position + offset; 
        //targetPosition.x = 0; 
        transform.position = targetPosition;
    }
}
