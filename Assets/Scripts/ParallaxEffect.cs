using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    [SerializeField] Transform followingTarget;
    [SerializeField, Range(0f, 1f)] float paralaxxValue= 0.1f;
    [SerializeField] bool disableVerticalParalax;
    float targetPrevPosition;
    void Start()
    {
        targetPrevPosition = followingTarget.position.x;
    }

    
    void FixedUpdate()
    {
        MovingBackground();
    }


    void MovingBackground(){
        var delta = (followingTarget.position.x - targetPrevPosition);
        
        targetPrevPosition = followingTarget.position.x;
        transform.Translate((delta * paralaxxValue), 0,0);
    }
}
