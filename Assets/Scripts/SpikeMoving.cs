using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeMoving : MonoBehaviour
{
    public float upperLimit = -2.1f;
    public float lowerLimit = -10f;
    public float speed = 2.0f;
    Vector3 movement;
    private int direction = 1;
    
    void FixedUpdate() {
        if (transform.position.y > upperLimit) {
            direction = -1;
            speed = 2f;
        }
        else if (transform.position.y < lowerLimit) {
            direction = 1;
            speed = 20f;
        }
        movement = Vector3.up * direction * speed * Time.deltaTime; 
        transform.Translate(movement); 
    }
    
      
}
