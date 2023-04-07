using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletRework : MonoBehaviour
{
    Rigidbody2D myRigidBody;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float bulletDestructionTime;
    EnemyWithGun enemy; 
    float xSpeed;
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        enemy = FindObjectOfType<EnemyWithGun>();
        xSpeed = enemy.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        if(enemy != null){
            myRigidBody.velocity = new Vector2(xSpeed, 0f);
            transform.localScale = new Vector3(enemy.transform.localScale.x, 1f, 1f);
        }
        
    }

    
    

    void OnTriggerEnter2D(Collider2D other){
        
        if(other.tag == "Player"){
            Destroy(gameObject);  
            Debug.Log("op1");
        }
        else if(other.tag == "Ground"){

            Destroy(gameObject);
            Debug.Log("op");
        } 
    }
}
