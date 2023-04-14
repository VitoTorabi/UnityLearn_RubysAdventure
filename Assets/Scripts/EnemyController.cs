using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 1.0f;
    Rigidbody2D rigidbody2D;
    Animator animator;
    public bool vertical;
    public float changeTime = 3.0f;
    float timer = 0;
    int direction = 1;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = rigidbody2D.position;
        timer -= Time.deltaTime;
        if (vertical) {
            position.y = position.y + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", 0);
            animator.SetFloat("Move Y", direction);
        }
        else {
            position.x = position.x + Time.deltaTime * speed * direction;
            animator.SetFloat("Move X", direction);
            animator.SetFloat("Move Y", 0);
        }
        if (timer < 0) {
            direction = -direction;
            timer = changeTime;
        }
        rigidbody2D.MovePosition(position);
    }
    void OnTriggerStay2D(Collider2D other)
    {
        RubyController controller = other.GetComponent<RubyController>();

        if (controller != null) {
            controller.ChangeHealth(-1);
        }
    }
}
