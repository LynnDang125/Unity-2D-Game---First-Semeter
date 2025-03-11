using UnityEngine;

public class Enemy : MonoBehaviour

{
    protected Rigidbody2D rb;
    protected Collider2D coll;
    protected Animator anim;
    private AudioSource deathAudio;

    //allows children to have access, virtual allows override
    protected virtual void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
        deathAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void JumpOn()
    {
        anim.SetTrigger("Death");
        deathAudio.Play();
        rb.linearVelocity = Vector2.zero; // stops enemy from moving
    }

    // Destroy the game object
    private void Death()
    {
        Destroy(this.gameObject);
    }
}
