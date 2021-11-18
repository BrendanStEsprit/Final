using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{
    public float speed;
    public int damage;
    public bool vertical;
    public float changeTime = 3.0f;
    public ParticleSystem smokeEffect;
    public AudioClip takeDamage;
    new Rigidbody2D rigidbody2D;
    float timer;
    int direction = 1;
    bool broken = true;
    private RubyControler rubyController;

    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        timer = changeTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if (!broken)
        {
            return;
        }

        timer -= Time.deltaTime;

        if (timer < 0)
        {
            direction = -direction;
            timer = changeTime;
        }
        GameObject rubyControllerObject = GameObject.FindWithTag("RubyController"); //this line of code finds the RubyController script by looking for a "RubyController" tag on Ruby

        if (rubyControllerObject != null)

        {

            rubyController = rubyControllerObject.GetComponent<RubyControler>(); //and this line of code finds the rubyController and then stores it in a variable

            

        }

        if (rubyController == null)

        {

            

        }
    }

    void FixedUpdate()
    {
        //remember ! inverse the test, so if broken is true !broken will be false and return won’t be executed.
        if (!broken)
        {
            return;
        }

        Vector2 position = rigidbody2D.position;

        if (vertical)
        {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else
        {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }

        rigidbody2D.MovePosition(position);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        RubyControler player = other.gameObject.GetComponent<RubyControler>();

        if (player != null)
        {
            player.ChangeHealth(-damage);
            //player.PlaySound(takeDamage);
            
        }

    }

    //Public because we want to call it from elsewhere like the projectile script
    public void Fix()
    {
        broken = false;
        rigidbody2D.simulated = false;
        rubyController.changescore(+1);
        //optional if you added the fixed animation
        animator.SetTrigger("Fixed");
        smokeEffect.Stop();
        
        
    }
}
