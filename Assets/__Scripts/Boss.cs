using UnityEngine;
using System.Collections;

public class Boss : MonoBehaviour
{

    // speed is 1.4 units. I.e. it takes 1.4/10 frames to move 1 unit 

    [SerializeField] float m_speed = 1.96f;
    [SerializeField] bool m_noBlood = false;

    public int enemyCurrentHealth = 100;

    private Animator m_animator;
    private SpriteRenderer m_SpriteRenderer;
    private Rigidbody2D m_body2d;
    private Sensor_HeroKnight m_groundSensor;
    private Sensor_HeroKnight m_wallSensorR1;
    private Sensor_HeroKnight m_wallSensorR2;
    private Sensor_HeroKnight m_wallSensorL1;
    private Sensor_HeroKnight m_wallSensorL2;
    private bool m_grounded = false;
    private int m_facingDirection = 1;
    private int m_currentAttack = 0;
    private float m_timeSinceAttack = 0.0f;
    private float m_delayToIdle = 0.0f;

    private float xOffsetHitbox = 1.5f;
    private float yOffsetHitbox = 0.662f;
    private float xOffsetHitboxBoss = 5f;
    private float xOffsetHitboxShieldAttack = 1f;
    private float yOffsetHitboxShieldAttack = 0.662f;

    private float inputX = -1;

    public HeroKnight player;
    public GameObject playerObject;

    public bool deadState = false;
    public bool isBoss = false;
    public bool isFlipped = false;

    // Use this for initialization
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_body2d = GetComponent<Rigidbody2D>();
        m_groundSensor = transform.Find("GroundSensor").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR1 = transform.Find("WallSensor_R1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorR2 = transform.Find("WallSensor_R2").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL1 = transform.Find("WallSensor_L1").GetComponent<Sensor_HeroKnight>();
        m_wallSensorL2 = transform.Find("WallSensor_L2").GetComponent<Sensor_HeroKnight>();

        m_SpriteRenderer = GetComponent<SpriteRenderer>();
        playerObject = GameObject.Find("HeroKnight");
        player = playerObject.GetComponent<HeroKnight>();
        
        if (this.gameObject.name == "Boss(Clone)") 
            isBoss = true;

        GetComponent<SpriteRenderer>().flipX = true;
        m_facingDirection = 1;
    }

    // Update is called once per frame
    void Update()
    {
        // Increase timer that controls attack combo
        m_timeSinceAttack += Time.deltaTime;
        //Check if character just landed on the ground
        if (!m_grounded && m_groundSensor.State())
        {
            m_grounded = true;
            m_animator.SetBool("Grounded", m_grounded);
        }

        //Check if character just started falling
        if (m_grounded && !m_groundSensor.State())
        {
            m_grounded = false;
            m_animator.SetBool("Grounded", m_grounded);
        }


        // -- Handle Animations --

        // handle being attacked. Bilateral hitbox (works when enemy is attacking from left or right)
        // if (player.isAttacking && !deadState && m_timeSinceAttack > 0.4f && Mathf.Abs(this.transform.position.x - playerObject.transform.position.x) < xOffsetHitbox && Mathf.Abs(this.transform.position.y - playerObject.transform.position.y) < yOffsetHitbox)
        // {
        //     ObjectTakeDamage(25, false);
        //     m_body2d.velocity = new Vector2(m_facingDirection * 3f, 2f);
        // } else if (player.isBlocking && player.shieldCombatMode && !deadState && m_timeSinceAttack > 0.4f && Mathf.Abs(this.transform.position.x - playerObject.transform.position.x) < xOffsetHitboxShieldAttack && Mathf.Abs(this.transform.position.y - playerObject.transform.position.y) < yOffsetHitboxShieldAttack) {
        //     ObjectTakeDamage(15, true);
        //     DealDamageToPlayer(10);
        //     m_body2d.velocity = new Vector2(m_facingDirection * 2f, 1.5f);
        // }

        // handle being attacked. Bilateral hitbox (works when enemy is attacking from left or right)
        // if (player.isAttacking && !deadState && m_timeSinceAttack > 0.4f && Mathf.Abs(this.transform.position.x - playerObject.transform.position.x) < xOffsetHitbox && Mathf.Abs(this.transform.position.y - playerObject.transform.position.y) < yOffsetHitbox)
        // {
        //     ObjectTakeDamage(25, false);
        //     m_body2d.velocity = new Vector2(m_facingDirection * 3f, 2f);
        // } else if (player.isBlocking && player.shieldCombatMode && !deadState && m_timeSinceAttack > 0.4f && Mathf.Abs(this.transform.position.x - playerObject.transform.position.x) < xOffsetHitboxShieldAttack && Mathf.Abs(this.transform.position.y - playerObject.transform.position.y) < yOffsetHitboxShieldAttack) {
        //     ObjectTakeDamage(15, true);
        //     DealDamageToPlayer(10);
        //     m_body2d.velocity = new Vector2(m_facingDirection * 2f, 1.5f);
        // }

        // handle being attacked. Bilateral hitbox (works when enemy is attacking from left or right)
        // if (isBoss && player.isAttacking && !deadState && m_timeSinceAttack > 0.4f && Mathf.Abs(this.transform.position.x - playerObject.transform.position.x) < xOffsetHitboxBoss)
        // {
        //     ObjectTakeDamage(25, false);
        //     m_body2d.velocity = new Vector2(m_facingDirection * 3f, 2f);
        // } else if (isBoss && player.isBlocking && player.shieldCombatMode && !deadState && m_timeSinceAttack > 0.4f && Mathf.Abs(this.transform.position.x - playerObject.transform.position.x) < xOffsetHitboxBoss && Mathf.Abs(this.transform.position.y - playerObject.transform.position.y) < yOffsetHitboxShieldAttack) {
        //     ObjectTakeDamage(15, true);
        //     DealDamageToPlayer(10);
        //     m_body2d.velocity = new Vector2(m_facingDirection * 2f, 1.5f);
        // }
    }

    void DeleteEnemy()
    {
        Destroy(this.gameObject);
    }

    // Animation Events
    // Called in slide animation.

    public void DealDamageToPlayer(int amount) {
        print(amount + "we in here");
        if(!player.isBlocking && !player.deadState && m_timeSinceAttack > 0.4f){
            print(amount +"attacking hero");
            playerObject.GetComponent<HeroHealth>().ObjectTakeDamage(10);
        }

        else if(player.isAttacking && !deadState && m_timeSinceAttack > 0.4f){
            print("we in here");
            ObjectTakeDamage(15,true);
        }
    }

    void HealPlayer(int amount) {
        playerObject.GetComponent<HeroHealth>().Heal(amount);
    }


    public void ObjectTakeDamage(int amount, bool shieldKill)
    {
        enemyCurrentHealth -= amount; // subtract health
        print("damage");

        // if (!isBoss) 
        //     m_animator.SetTrigger("Hurt");

        if (enemyCurrentHealth <= 0)
        {
            deadState = true;
            
            if (isBoss) {
                GameObject.Find("Main Camera").GetComponent<EnemySpawner>().UpdatePoints(500);
            } else if (!isBoss) {
                GameObject.Find("Main Camera").GetComponent<EnemySpawner>().UpdatePoints(150);
            }

            if (shieldKill) {
                HealPlayer(20);
            } else {
                HealPlayer(35);
            }
            // if(!isBoss)
            //     m_animator.SetTrigger("die");
            
            Invoke("DeleteEnemy", 1f);
        }
        m_timeSinceAttack = 0;
    }

    public void LookAtPlayer(){
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;
        print(playerObject.transform.position.x);
        if(transform.position.x > playerObject.transform.position.x && isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = false;
        }

        else if( transform.position.x < playerObject.transform.position.x && !isFlipped){
            transform.localScale = flipped;
            transform.Rotate(0f,180f,0f);
            isFlipped = true;
        }
    }
}
