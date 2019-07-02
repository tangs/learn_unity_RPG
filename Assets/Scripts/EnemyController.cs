using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float moveLen = 3.0f;
    public float moveSpeed = 3f;
    public int att = 1;
    public bool isHorizontal = true;
    private Vector2 oriPos;
    Rigidbody2D rigidbody2d;
    bool isForth;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        oriPos = rigidbody2d.position;
        isForth = true;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = rigidbody2d.position;
        float delta = (isForth ? 1 : -1) * Time.deltaTime * moveSpeed;
        if (isHorizontal) 
        {
            pos.x += delta;
        }
        else
        {
            pos.y += delta;
        }
        rigidbody2d.MovePosition(pos);
        if (isForth) 
        {
            if (pos.x + pos.y >= oriPos.x + oriPos.y + moveLen)
            {
                isForth = false;
            }
        } 
        else 
        {
            if (pos.x + pos.y <= oriPos.x + oriPos.y)
            {
                isForth = true;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other) 
    {
        RubyController controller = other.gameObject.GetComponent<RubyController>();
        if (controller != null) 
        {
            controller.ChangeHealth(-1 * att);
        }
    }
}
