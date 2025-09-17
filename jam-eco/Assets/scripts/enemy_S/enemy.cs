using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public float speed = 2f;
    public float distanceJoint;
    public Transform target;
    public bool right = true;

    public string tagIgnorar = "Ignorar"; // tag que o inimigo deve ignorar

    private Collider2D enemyCollider;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        enemyCollider = GetComponent<Collider2D>(); 
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > distanceJoint)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.position.x, target.position.y), speed * Time.deltaTime);
        }

        if ((transform.position.x - target.position.x) > 0 && !right)
        {
            vire();
        }
        else if (transform.position.x - target.position.x < 0 && !right)
        {
            vire();
        }
    }

    void vire()
    {
        right = !right;
        Vector2 newScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        transform.localScale = newScale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(tagIgnorar))
        {
            Physics2D.IgnoreCollision(enemyCollider, collision.collider);
        }
    }
}
