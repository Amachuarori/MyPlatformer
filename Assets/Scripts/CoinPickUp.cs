using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour
{
    [SerializeField] AudioClip coinPickUpsfx;
    [SerializeField] int pointsForPickUp = 100;
    bool wasCollected = false;
    
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player" && !wasCollected){
            wasCollected = true;
            //FindObjectOfType<GameSession>().AddToScore(pointsForPickUp);
            Destroy(gameObject);
            AudioSource.PlayClipAtPoint(coinPickUpsfx, other.transform.position);
        }
    }
}
