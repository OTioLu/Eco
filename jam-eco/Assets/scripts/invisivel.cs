using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisivel : MonoBehaviour
{
    public float raio = 5f;
    public string invisivelTag = "invisivel";
    public KeyCode KeyCodeToPress = KeyCode.E;
    public float tempoinvisivel = 2f;
    public float tempoCooldown = 1f; // tempo de espera do E

    private bool podeApertar = true;

    private void Start()
    {
        
        GameObject[] invisivelObjects = GameObject.FindGameObjectsWithTag(invisivelTag);
        foreach (GameObject obj in invisivelObjects)
        {
            var spriteRenderer = obj.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null) spriteRenderer.enabled = false;
        }
    }

    private void Update()
    {
        if (podeApertar && Input.GetKeyDown(KeyCodeToPress))
        {
            
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, raio);

           
            StartCoroutine(MostrarTemporariamente(colliders));
        }
    }

    IEnumerator MostrarTemporariamente(Collider2D[] colliders)
    {
        podeApertar = false;

        // Ativa só os que estão no raio e têm a tag "invisivel"
        List<GameObject> revelados = new List<GameObject>();
        foreach (Collider2D col in colliders)
        {
            if (col.CompareTag(invisivelTag))
            {
                var renderer = col.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.enabled = true;
                    revelados.Add(col.gameObject); 
                }
            }
        }

       
        yield return new WaitForSeconds(tempoinvisivel);

       
        foreach (GameObject obj in revelados)
        {
            var renderer = obj.GetComponent<Renderer>();
            if (renderer != null) renderer.enabled = false;
        }

      
        yield return new WaitForSeconds(tempoCooldown);
        podeApertar = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, raio);
    }
}
