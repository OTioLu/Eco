using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coletor : MonoBehaviour
{
    
    public int objetosNecessarios = 3;       
    private int objetosColetados = 0;        
    public GameObject objetoParaDesativar;   
    public string tagColetavel = "Coletavel"; // Tag dos itens coletáveis

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tagColetavel))
        {
            objetosColetados++;
            Debug.Log("Coletados: " + objetosColetados);

            Destroy(other.gameObject); 

            if (objetosColetados >= objetosNecessarios)
            {
                objetoParaDesativar.SetActive(false);
                Debug.Log("Condição atingida! Objeto desativado.");
            }
        }
    }
}

