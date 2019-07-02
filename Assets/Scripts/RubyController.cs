using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    public int maxHealth = 5;
    public float timeInvincible = 2.0f;

    int curHealth;
    bool isInvincible;
    float invincibleTimer;
    public int health { get { return curHealth; }}
    // private int dir;
    // Start is called before the first frame update

    Rigidbody2D rigidbody2d;

    void Start()
    {
        // QualitySettings.vSyncCount = 0;
        // Application.targetFrameRate = 10;
        // dir = transform.position.x < 0 ? 0 : 1;
        rigidbody2d = GetComponent<Rigidbody2D>();
        curHealth = maxHealth;
        isInvincible = false;
        invincibleTimer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = rigidbody2d.position;
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        // Debug.Log(horizontal);
        pos.x += speed * horizontal * Time.deltaTime;
        pos.y += speed * vertical * Time.deltaTime;
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

    public void ChangeHealth(int amount) {
        if (amount < 0) 
        {
            if (isInvincible) 
            {
                return;
            }
            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        curHealth = Mathf.Clamp(curHealth + amount, 0, maxHealth);
        Debug.Log(curHealth + "/" + maxHealth);
    }
}
