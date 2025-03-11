 using UnityEngine;

public class Frog : Enemy
{
    [SerializeField]private float rightCap;
    [SerializeField]private float leftCap;
    [SerializeField]private float jumpLength =10f;
    [SerializeField]private float jumpHeight =15f;
    [SerializeField]private LayerMask ground;

    private bool facingLeft = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    { 
        // Jumping to falling state
        if(anim.GetBool("Jumping")){
            // If our velocity is less than 0.1, set jumping to false and falling to true
            if(rb.linearVelocity.y < .1){
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
                
            }
        } 
        // Falling to idle state
        // If the frog is falling and it is touching the ground, set falling to false
        if(coll.IsTouchingLayers(ground) && anim.GetBool("Falling")){
            anim.SetBool("Falling", false);
            anim.SetBool("Jumping", false);
        } 
    }

    private void move(){
        if (facingLeft){
            // Test to see if we are beyond the leftCap
            if(transform.position.x > leftCap){
                // Make sure the sprite is facing the right direction
                if (transform.localScale.x != 1){
                    transform.localScale = new Vector3(1,1);
                }
                // If the frog is on the ground
                if(coll.IsTouchingLayers(ground)){
                    // Jump to the left
                    rb.linearVelocity = new Vector2(-jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
                
            } else {
                facingLeft = false;
            }
        }
        else{
            // Test to see if we are beyond the rightCap
            if(transform.position.x < rightCap){
                // Make sure the sprite is facing the right direction
                if (transform.localScale.x != -1){
                    transform.localScale = new Vector3(-1,1);
                }
                // If the frog is on the ground
                if(coll.IsTouchingLayers(ground)){
                    // Jump to the right
                    rb.linearVelocity = new Vector2(jumpLength, jumpHeight);
                    anim.SetBool("Jumping", true);
                }
                
            } else {
                facingLeft = true;
            }
        } 
    }

}
