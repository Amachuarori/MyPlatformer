using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [Header("Speed")]
    [SerializeField] float runSpeed = 10f;
    [SerializeField] float jumpSpeed = 20f;
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
    
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<BoxCollider2D>();
        myAnimator = GetComponent<Animator>();
        bodyCollider = GetComponent<CapsuleCollider2D>();
        audioS = GetComponent<AudioSource>();
    }

    void Update()
    {
        Alive();
    }

    void Alive(){
        if(isAlive){
            Run();
            FlipSprite();
            StartCoroutine(Die());
        }
        
    }

    IEnumerator Die(){
        if(bodyCollider.IsTouchingLayers(LayerMask.GetMask("Hazards", "Enemy"))){
            isAlive = false;
            myRigidBody.velocity = deathKick;
            myAnimator.SetTrigger("Died");
            yield return new WaitForSecondsRealtime(2);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
        if(value.isPressed && (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))){
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
