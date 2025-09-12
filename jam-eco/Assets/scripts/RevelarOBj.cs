using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class RevelarOBj : MonoBehaviour

{
    public float raio = 5f;           
    public float tempoReveladoOBJ = 2f;  
    public float tempoAtivoNPC = 3f;      

    private AudioSource audioSource;

    void OnEnable() // chamado quando o NPC é ativado
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null) audioSource.Play();

        StartCoroutine(RevelarObjetos());
        StartCoroutine(DesativarDepoisNPC());
    }

    IEnumerator RevelarObjetos()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, raio);

        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("invisivel"))
            {
                var renderer = col.GetComponent<Renderer>();
                if (renderer != null) renderer.enabled = true;
            }
        }

        yield return new WaitForSeconds(tempoReveladoOBJ);

        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag("invisivel"))
            {
                var renderer = col.GetComponent<Renderer>();
                if (renderer != null) renderer.enabled = false;
            }
        }
    }

    IEnumerator DesativarDepoisNPC()
    {
        yield return new WaitForSeconds(tempoAtivoNPC);
        gameObject.SetActive(false); 
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raio);
    }
}
