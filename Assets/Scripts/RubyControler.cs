using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class RubyControler : MonoBehaviour
{
    public int maxHealth = 5;
    public float  speed;
    public float timeInvincible = 2.0f;
    float dashtimer;
    float inputsensitivity = 0.5F;
    float dashcooldown;
    bool dashoncooldown;
    bool abletodashLeft;
    bool abletodashRight;
    bool abletodashUp;
    bool abletodashDown;
    bool isdashing;
    bool touchingwater;
    float dashtimerRight;
    float dashtimerLeft;
    float dashtimerUp;
    float dashtimerDown;
    public int Health { get { return currentHealth; } }
    int currentscore;
    int currentHealth;
    int maxscore = 4;
    int Cog;
    public static int level;
    public Text cogtext;
    public Text score;
    public Text WinText;
    public Text DashText;
    public Text Tutorial;
    bool isInvincible;
    bool isdefeated;
    bool sheildpower;
    bool playedmusic;
    float invincibleTimer;
    float horizontal;
    float vertical;
    Rigidbody2D rigidbody2d;
    Animator animator;
    public GameObject projectilePrefab;
    public GameObject HealthParticle;
    public GameObject DamageParticle;
    public GameObject BackgroundMusic;
    public GameObject Sheild;
    public GameObject Fire;
    AudioSource audioSource;
    Vector2 lookDirection = new Vector2(1, 0);
    Vector2 DashRight = new Vector2(1,0);
    Vector2 DashLeft = new Vector2(-1, 0);
    public AudioClip takeDamage;
    public AudioClip Win;
    public AudioClip Lose;
    public AudioClip Throw;
    public AudioClip PickUp;
    public AudioClip splash;
    public AudioClip fire;
    

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = 3;
        currentHealth = maxHealth;
        score.text = currentscore.ToString();
        cogtext.text = Cog.ToString();
        DashText.text = dashcooldown.ToString();
        
        
        audioSource = GetComponent<AudioSource>();
        
        WinText.text = "";
        Tutorial.text = "double tap a direction to dash, dashing fixes robots";
        isdefeated = false;
        playedmusic = false;
        dashcooldown = 2;
        touchingwater = false;
        isdashing = false;
        
    }

    // Update is called once per frame
    void Update()
    {

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        Vector2 move = new Vector2(horizontal, vertical);
        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f))
        {
            
                lookDirection.Set(move.x, move.y);
                lookDirection.Normalize();
            
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);


        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;

        }
        if(dashoncooldown)
        {
            DashText.text = dashcooldown.ToString();
            dashcooldown -= Time.deltaTime;
            if (dashcooldown < 0)
                dashoncooldown = false;
            
        }
        
        {
            
            
            
            
            if (abletodashLeft)
            {
                dashtimerLeft -= Time.deltaTime;
                if (dashtimerLeft < 0)
                {
                    abletodashLeft = false;
                    isdashing = false;
                    speed = 3;
                }
            }
            if (abletodashRight)
            {
                dashtimerRight -= Time.deltaTime;
                if (dashtimerRight < 0)
                {
                    abletodashRight = false;
                    isdashing = false;
                    speed = 3;
                }
            }
            if (abletodashUp)
            {
                dashtimerUp -= Time.deltaTime;
                if (dashtimerUp < 0)
                {
                    abletodashUp = false;
                    isdashing = false;
                    speed = 3;
                }
            }
            if (abletodashDown)
            {
                dashtimerDown -= Time.deltaTime;
                if (dashtimerDown < 0)
                {
                    abletodashDown = false;
                    isdashing = false;
                    speed = 3;
                }
            }
        }
        if (dashtimerLeft > 0)
        { abletodashLeft = true; }
        if (dashtimerRight > 0)
        { abletodashRight = true; }
        if (dashtimerUp > 0)
        { abletodashUp = true; }
        if (dashtimerDown > 0)
        { abletodashDown = true; }
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (isdefeated)
                return;
            if(Cog > 0)
            {
                Launch();
                Cog -= 1;
                cogtext.text = Cog.ToString();
            }
        }
        if (Input.GetKey("escape"))
        {
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                if (hit.collider != null)
                {
                    NonPlayerCharacter character = hit.collider.GetComponent<NonPlayerCharacter>();
                    if (character != null)
                    {
                        character.DisplayDialog();
                    }
                    if (currentscore >= 4)
                    {
                        SceneManager.LoadScene("Scene2");
                        level = 2;
                        playedmusic = true;
                    }
                }
            }
        }
        if (currentscore >= 4)
        {
            if (level==2)
            {
                WinText.text = "You Win, Made by Brendan";
                if (playedmusic == false)
                {
                    PlaySound(Win);
                    playedmusic = true;


                }
                BackgroundMusic.SetActive(false);
            }
            

        }
        if (currentHealth <= 0)
        {
            isdefeated = true;
            WinText.text = " you lost bro, press R to restart";
            
            BackgroundMusic.SetActive(false);
            if (playedmusic == false)
            {
                PlaySound(Lose);
                playedmusic = true;


            }
        }
        if (Input.GetKey(KeyCode.R))

        {

            if (isdefeated == true)

            {

                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // this loads the currently active scene

            }

        }
        if (!dashoncooldown)
        {
            DashText.text = "Ability Ready";
            if (Input.GetKeyDown(KeyCode.D))
            {
                if (horizontal >0.5F)
                {
                    if (!isdashing)
                    {
                        

                        if (abletodashRight)
                        {
                            speed = 6.4F;
                            horizontal = 0.6F;
                            PlaySound(fire);
                            isdashing = true;
                            dashcooldown = 2.0F;
                            dashoncooldown = true;
                            abletodashRight = false;
                        }
                    }
                }
                else
                {

                }
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                dashtimerRight = 0.35F;
                abletodashRight = true;


            }
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (vertical > 0.5)
                {
                    if (!isdashing)
                    {


                        if (abletodashUp)
                        {
                            speed = 6.4F;
                            vertical = 0.6F;
                            PlaySound(fire);
                            isdashing = true;
                            dashcooldown = 2.0F;
                            dashoncooldown = true;
                            abletodashUp = false;
                            //dashdirection = 2;
                        }
                    }
                }
                else
                {

                }
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                dashtimerUp = 0.35F;
                abletodashUp = true;


            }
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (horizontal < -0.5F)
                {
                    if (!isdashing)
                    {


                        if (abletodashLeft)
                        {
                            speed = 6.4F;
                            horizontal = -0.6F;
                            PlaySound(fire);
                            isdashing = true;
                            dashcooldown = 2.0F;
                            dashoncooldown = true;
                            abletodashLeft = false;
                            //dashdirection = 3;
                        }
                    }
                }
                else
                {

                }
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                dashtimerLeft = 0.35F;
                abletodashLeft = true;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                if (vertical < -0.5)
                {
                    if (!isdashing)
                    {

                        if (abletodashDown)
                        {
                            vertical = -1;
                            speed = 6.4f;
                            PlaySound(fire);
                            vertical = -0.6F;
                            isdashing = true;
                            dashcooldown = 2.0F;
                            dashoncooldown = true;
                            abletodashDown = false;
                            //dashdirection = 4;
                        }
                    }
                }
                else
                {

                }

            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                dashtimerDown = 0.35F;
                abletodashDown = true;


            }
            if(Input.GetKeyDown(KeyCode.A))
            {
                if (Input.GetKeyDown(KeyCode.D))
                {
                    return;
                }
            }
        }
        


    }
    private void FixedUpdate()
    {
        if (isdefeated)
            return;



        if (touchingwater)
            return;
            Vector2 position = rigidbody2d.position;
            position.x = position.x + speed * horizontal * Time.deltaTime;
            position.y = position.y + speed * vertical * Time.deltaTime;
            rigidbody2d.MovePosition(position);
        
        
        if(sheildpower == true)
        {
            Sheild.SetActive(true);
        }
        if (sheildpower == false)
        {
            Sheild.SetActive(false);
        }




        if (isdashing)
        {
            
            Fire.SetActive(true);
            
            //isInvincible = true;
            Tutorial.text = "";

        }
        if(!isdashing)
        {
            Fire.SetActive(false);
            
        }
        

    }
    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            
            if (sheildpower)
            {
                sheildpower = false;
                isInvincible = true;
                invincibleTimer = timeInvincible;
                return;
            }
            
            
                if (isInvincible)
                    return;
            
            
            
            isInvincible = true;
            invincibleTimer = timeInvincible;
            animator.SetTrigger("Hit");

            
            
                
                Instantiate(DamageParticle, rigidbody2d.position + Vector2.up, Quaternion.identity);
                PlaySound(takeDamage);
            
        }
        if (amount > 0)
        {
            Instantiate(HealthParticle, rigidbody2d.position + Vector2.up, Quaternion.identity);
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        
        
        UIHealthBar.instance.SetValue(currentHealth / (float)maxHealth);

    }
    public void changescore(int scorechange)
    {
        currentscore = Mathf.Clamp(currentscore + scorechange, 0, maxscore);
        score.text = currentscore.ToString();
    }
    void Launch()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);

        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        PlaySound(Throw);
    }
    public void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "CogPickup")
        {
            Cog += 1;
            Destroy(collision.collider.gameObject);
            cogtext.text = Cog.ToString();
            PlaySound(PickUp);
        }
        if (collision.collider.tag == "SheildPickup")
        {
            sheildpower = true;
        }
        if(collision.collider.tag == "Wall")
        {
            
            dashoncooldown =true;
            abletodashDown = false;
            abletodashLeft = false;
            abletodashRight = false;
            abletodashUp = false;
            isdashing = false;
            PlaySound(splash);
            
            speed = 3;
            if (level == 0)
            {
                rigidbody2d.transform.position = new Vector2(1, 1);
            }
            if(level ==2)
            {
                rigidbody2d.transform.position = new Vector2(-1, 1);
            }
            ChangeHealth(-1);
            
            isInvincible = false;
        }
    }
    


}
