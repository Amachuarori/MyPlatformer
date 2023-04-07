using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float upperLimit = -2.1f;
    public float lowerLimit = -10f;
    public float speed = 2.0f;
    Vector3 movement;
    private int direction = 1;
    
    void FixedUpdate() {
        if (transform.position.y > upperLimit) {
            direction = -1;
        }
        else if (transform.position.y < lowerLimit) {
            direction = 1;
        }
        movement = Vector3.up * direction * speed * Time.deltaTime; 
        transform.Translate(movement); 
    }
}
