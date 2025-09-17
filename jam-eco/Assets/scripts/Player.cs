using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;


public class Player : MonoBehaviour
{
    public Animator animator;
    public float speed = 5f;
    public int vida = 1;              
    public int danoDoInimigo = 1;     
    private string inimigoTag = "Enemy"; 
    public string Scene;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(inimigoTag))
        {
            TomarDano(danoDoInimigo);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(inimigoTag))
        {
            TomarDano(danoDoInimigo);
        }
    }

    void TomarDano(int dano)
    {
        vida -= dano;
      

        if (vida <= 0)
        {
            Morrer();
        }
    }

    void Morrer()
    {
        Debug.Log("O player morreu!");
        SceneManager.LoadScene(Scene);
        gameObject.SetActive(false);

    }
}