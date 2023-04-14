using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RubyController : MonoBehaviour
{
    public float speed = 3.0f;
    
    Rigidbody2D rigidbody2D;
    int currentHealth;
    public int health { get { return currentHealth; } }
    public int maxHealth = 5;
    protected Joystick joystick;

    public float timeInvincible = 2.0f;
    bool isInvincible;
    float invincibleTimer;
    Vector2 lookDirection = new Vector2(1, 0);
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        joystick = FindObjectOfType<Joystick>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        currentHealth = maxHealth-2;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = joystick.Horizontal + Input.GetAxis("Horizontal");
        float vertical = joystick.Vertical + Input.GetAxis("Vertical");
       

        Vector2 move = new Vector2(horizontal, vertical);

        if (!Mathf.Approximately(move.x, 0.0f) || !Mathf.Approximately(move.y, 0.0f)) {
            lookDirection.Set(move.x, move.y);
            lookDirection.Normalize();
        }

        animator.SetFloat("Look X", lookDirection.x);
        animator.SetFloat("Look Y", lookDirection.y);
        animator.SetFloat("Speed", move.magnitude);

        Vector2 position = rigidbody2D.position;

        position = position + move * speed * Time.deltaTime;

        rigidbody2D.MovePosition(position);

        if (isInvincible) {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0) {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;
        }
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log(currentHealth + "/" + maxHealth);
    }
}
