using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Player Stats")]
    [SerializeField] float climbSpeed =5f;
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 20f;
    [SerializeField] int hp = 3;
    [SerializeField] Vector2 deathKick = new Vector2(10f, 10f);

    [Header("GameObjects")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform gun;
    [SerializeField] AudioClip audioClip;

    Animator myAnimator;
    BoxCollider2D feetCollider;
    CapsuleCollider2D bodyCollider;
    
    Rigidbody2D myRigidBody;
    Vector2 moveInput;
    bool isAlive = true;
    AudioSource audioS;
    float gravityScaleAtStart;
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        audioS = GetComponent<AudioSource>();
        gravityScaleAtStart = myRigidBody.gravityScale;  
        
    }

    void Update()
    {
        Alive();
    }

    void Alive(){
        if(isAlive){
            Run();
            FlipSprite();
            ClimbLadder();
            StartCoroutine(Die());
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "EnemyBullet" && hp >=1){
            hp--;
            Destroy(other.gameObject);
            Debug.Log("Player HP " + hp);
        }
        if(other.tag == "EnemyBullet" && hp == 0){
            isAlive = false;
            Destroy(other.gameObject);
            StartCoroutine(Die());
        }
        if(other.tag == "Coin"){
            Destroy(other.gameObject);
        }
    }

    IEnumerator Die(){
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards", "Enemy"))){
            isAlive = false;
            myRigidBody.velocity = deathKick;
            myAnimator.SetTrigger("Died");
        }
        
        if(!isAlive){
            yield return new WaitForSecondsRealtime(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    void ClimbLadder()
    {
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Ladder"))){
            Debug.Log("op");
            Vector2 climbVelocity = new Vector2(myRigidBody.velocity.x, moveInput.y * climbSpeed);
            myRigidBody.velocity = climbVelocity;
            myRigidBody.gravityScale = 0;
            bool playerHasVerticalSpeed = Mathf.Abs(myRigidBody.velocity.y) > Mathf.Epsilon;
            myAnimator.SetBool("IsClimbing", playerHasVerticalSpeed);
        }
        else{
            
            myRigidBody.gravityScale = gravityScaleAtStart;
            myAnimator.SetBool("IsClimbing", false); 
        }    
    }
    
    void OnFire(InputValue value){
        if(isAlive){
            Instantiate(bullet, gun.position, transform.rotation);
            myAnimator.SetTrigger("BowAttack");
            audioS.PlayOneShot(audioClip);
        }
    }

    void OnMove(InputValue value){
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value){
        if(value.isPressed && (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")) 
        || feetCollider.IsTouchingLayers(LayerMask.GetMask("Platform"))))
        {
            myRigidBody.velocity += new Vector2(0f, jumpSpeed);
        }

        
    }

    void Run(){
        Vector2 playerVelocity = new Vector2(moveInput.x * runSpeed, myRigidBody.velocity.y);
        myRigidBody.velocity = playerVelocity;
        bool PlayerHasHorizontalSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("IsRunning", PlayerHasHorizontalSpeed);
    }

    void FlipSprite(){
        bool playerHasHorSpeed = Mathf.Abs(myRigidBody.velocity.x) > Mathf.Epsilon;
        if(playerHasHorSpeed){
        transform.localScale = new Vector2(Mathf.Sign(myRigidBody.velocity.x)*1.5f, 1.5f);
        }
    }
}
