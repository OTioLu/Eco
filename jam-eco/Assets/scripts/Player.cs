using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;
    public Transform NPC;
    public float interactRange = 1.2f;

    DialogueSystem dialogueSystem;

    private void Awake()
    {
        dialogueSystem = FindObjectOfType<DialogueSystem>();
    }

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

        if (NPC == null || dialogueSystem == null) return;

        if (Vector2.Distance(transform.position, NPC.position) <= interactRange)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                dialogueSystem.Interact();
            }
        }
    }
}