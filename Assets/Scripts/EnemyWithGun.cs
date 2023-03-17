using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWithGun : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    [SerializeField] int hp = 5;
    [SerializeField] ParticleSystem deadBubbler;
    [SerializeField] AudioClip deadBubblerClip;
    [SerializeField] Transform gun;
    [SerializeField] GameObject bullet;
    [SerializeField] float bulletInterval;
    float intervalShooting;
    float bulletDistructionTime = 1f;
    Animator anim;
    Rigidbody2D rbody;
    AudioSource audioSour;
    float scaleY;
    float scaleX;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        scaleY = transform.localScale.y;
        scaleX = transform.localScale.x;
        audioSour = GetComponent<AudioSource>();
        intervalShooting = bulletInterval;
    }
    void Update()
    {
        Moving();
        BulletInstantiating();
       
    }
    

    void BulletInstantiating(){
        intervalShooting -= (Time.deltaTime);
        if(intervalShooting < 0){
            Instantiate(bullet, gun.position, transform.rotation);
            intervalShooting = bulletInterval;
            while(bulletDistructionTime > 0){
                bulletDistructionTime -=(Time.deltaTime);
            }
            bulletDistructionTime = 1f;
        }
    }
    

    IEnumerator DeadState(){
        deadBubbler.Play();
        audioSour.PlayOneShot(deadBubblerClip);

        yield return new WaitForSecondsRealtime(1f);
        
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Bullet"){
            if(hp >1){
                hp--;
                Debug.Log(getHP());
                Destroy(other.gameObject);
            }
            else{
                moveSpeed= 0;
                Destroy(other.gameObject);
                StartCoroutine(DeadState());
            }
        }
        
    }

    public void setHP(){
        this.hp -= 1;
    }

    public int getHP(){
        return hp;
    }

    void Moving(){
        rbody.velocity = new Vector2(moveSpeed, 0f);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Ground"){
            moveSpeed = -moveSpeed;
            FlipEnemyFacing();  
        }
    }

    void FlipEnemyFacing(){ 
        transform.localScale = new Vector2(Mathf.Sign(moveSpeed)*scaleX, scaleY);
    }
}
