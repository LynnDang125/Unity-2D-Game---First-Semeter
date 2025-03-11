using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class playerControl : MonoBehaviour
{
    // Start() variables
    private Rigidbody2D body;
    private Collider2D collier;
    private Animator animator;
    private Collision2D collision;
    // Finite state machine
    private enum State
    {
        idle,
        running,
        jumping,
        falling,
        hurting
    }
    private State state = State.idle;

    // Inspector variables
    [SerializeField] private LayerMask ground;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private int cherries = 0;
    [SerializeField] private Text cherryText;
    [SerializeField] private float hurtForce = 5f;
    [SerializeField] private AudioSource footstep;
    [SerializeField] private AudioSource collected;
    [SerializeField] private Text healthAmount;
    [SerializeField] private int health;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        collier = GetComponent<Collider2D>();
        healthAmount.text = health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if (state != State.hurting)
        {
        Movement();
        }
        AnimatonState();
        // set the animation based on the enum state
        animator.SetInteger("state", (int)state);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Collectable")
        {
            
            Destroy(other.gameObject);
            collected.Play();
            cherries++;
            cherryText.text = cherries.ToString();
        
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       if (other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            if (state == State.falling){ 
                enemy.JumpOn();
                Jump();
            }
            else
            {
                state = State.hurting;
                HandleHealth();
                if(other.gameObject.transform.position.x > transform.position.x)
                {
                    // enemy is to my right therefore I should be damaged and move left
                    body.linearVelocity = new Vector2(-hurtForce, body.linearVelocity.y);
                }
                else
                {
                    // enemy is to my left therefore I should be damaged and move right
                    body.linearVelocity = new Vector2(hurtForce, body.linearVelocity.y);
                }
            } 
               
        }

        /* if (other.gameObject.tag == "Enemy"){
             Destroy(other.gameObject);
        } */
    }
    //Decrease health, update health text and restart level if health is 0
    private void HandleHealth()
    {
        health--;
        healthAmount.text = health.ToString();
        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
    private void Movement(){
         float x = Input.GetAxis("Horizontal");
        
        // moving left
        if (x<0){
            body.linearVelocity = new Vector2(-speed, body.linearVelocity.y);
            transform.localScale = new Vector2(-1, 1);
        }
        
        // moving right
        else if (x>0){
            body.linearVelocity = new Vector2(speed, body.linearVelocity.y);
            transform.localScale = new Vector2(1, 1);
        }

        // jumping
        if (Input.GetButtonDown("Jump") && collier.IsTouchingLayers(ground)){
            Jump();
        } 
       
        
    }


    private void Jump()
{
    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpForce);
    state = State.jumping;
}
    private void AnimatonState()
    {
        
        if (state == State.jumping)
        {
            if (body.linearVelocity.y < .1f)
            {
                state = State.falling;
            }
        } 
        else if (state == State.falling)
        {
            if (collier.IsTouchingLayers(ground))
            {
                state = State.idle;
            }
        }
        else if (state == State.hurting)
        {
            if (Mathf.Abs(body.linearVelocity.x) < .1f)
            {
                state = State.idle;
            }
        }
        else if (Mathf.Abs(body.linearVelocity.x) > 2f)
        {
            state = State.running;
        }
        else{
            state = State.idle;
        }

    }

    private void Footstep()
    {
        footstep.Play();
    }
       
}
