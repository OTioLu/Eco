using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;



    void Update()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);

        if (animator != null)
        {
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
            animator.SetFloat("Speed", movement.magnitude);
        }

        transform.position = transform.position + movement * speed * Time.deltaTime;


    }
}