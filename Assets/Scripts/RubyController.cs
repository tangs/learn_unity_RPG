using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;
    public float fireWaitTime = 2.0f;
    public Object projectilePrefab;
    public ParticleSystem bombPrefab;
    public ParticleSystem CurePrefab;

    int curHealth;
    bool isInvincible;
    float invincibleTimer;
    Vector2 lookDirection;
    private float lastFireTime = -1.0f;
    public int health { get { return curHealth; }}
    // private int dir;
    // Start is called before the first frame update

    Rigidbody2D rigidbody2d;
    Animator animator;

    void Start()
    {
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 10;
        // dir = transform.position.x < 0 ? 0 : 1;
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        curHealth = maxHealth;
        isInvincible = false;
        invincibleTimer = 0.0f;
        lookDirection = new Vector2(-0.1f, 0f);
        lookDirection.Normalize();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = rigidbody2d.position;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        //if (Input.GetKeyUp(KeyCode.Space)) {
        //    // animator.SetBool("Hit", true);
        //    //animator.SetTrigger("Launch");
        //    Launch();
        //}
        float fire = Input.GetAxis("Fire1");
        if (!Mathf.Approximately(fire, 0.0f))
        {
            Launch();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            RaycastHit2D hit = Physics2D.Raycast(rigidbody2d.position + Vector2.up * 0.2f, lookDirection, 1.5f, LayerMask.GetMask("NPC"));
            if (hit.collider != null)
            {
                //Debug.Log("Raycast has hit the object " + hit.collider.gameObject);
                NonPlayerCharacter npc = hit.collider.gameObject.GetComponent<NonPlayerCharacter>();
                if (npc != null)
                {
                    npc.DisplayDialog();
                }
            }
        }
        // Debug.Log(horizontal);
        pos.x += speed * horizontal * Time.deltaTime;
        pos.y += speed * vertical * Time.deltaTime;
        float moveSpeed = Mathf.Sqrt(horizontal * horizontal + vertical * vertical);
        //Debug.Log("move speed:" + moveSpeed + "," + horizontal + "," + vertical);
        if (!Mathf.Approximately(0.0f, horizontal) || !Mathf.Approximately(0.0f, vertical)) {
            lookDirection.Set(horizontal, vertical);
            lookDirection.Normalize();
        }
        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", moveSpeed);
        // transform.position = pos;
        rigidbody2d.MovePosition(pos);

        if (isInvincible) 
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer <= 0) 
            {
                isInvincible = false;
            }
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0) 
        {
            if (isInvincible) 
            {
                return;
            }

            animator.SetTrigger("Hit");
            isInvincible = true;
            invincibleTimer = timeInvincible;

            Instantiate(bombPrefab, rigidbody2d.position + Vector2.up * 0.5f,
                Quaternion.identity);
        }
        else if (amount > 0)
        {
            Instantiate(CurePrefab, rigidbody2d.position + Vector2.up * 0.5f,
               Quaternion.identity);
        }
        curHealth = Mathf.Clamp(curHealth + amount, 0, maxHealth);
        //Debug.Log(curHealth + "/" + maxHealth);
        UIHealthBar.instance.SetValue((float)curHealth / maxHealth);
    }

    void Launch()
    {
        float time = Time.time;
        if (!Mathf.Approximately(lastFireTime, -1f) && time - lastFireTime < fireWaitTime)
        {
            return;
        }
        GameObject projectileObject = (GameObject)Instantiate(projectilePrefab,
            rigidbody2d.position + Vector2.up * 0.5f, Quaternion.identity);
        Projectile projectile = projectileObject.GetComponent<Projectile>();
        projectile.Launch(lookDirection, 300);
        animator.SetTrigger("Launch");
        lastFireTime = time;
    }
}
