using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class invisivel : MonoBehaviour
{
    public string invisivelTag = "invisivel";
    public KeyCode KeyCodeToPress = KeyCode.E;
    public float tempoinvisivel = 2f;
    public float tempoCooldown = 1f; // tempo de espera do E

    private GameObject[] invisivelObjects;
    private bool podeApertar = true;

    void Start()
    {
        invisivelObjects = GameObject.FindGameObjectsWithTag(invisivelTag);

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
            StartCoroutine(MostrarTemporariamente());
        }
    }

    IEnumerator MostrarTemporariamente()
    {
        podeApertar = false;

        // Ativa
        foreach (GameObject obj in invisivelObjects)
        {
            var renderer = obj.GetComponent<Renderer>();
            if (renderer != null) renderer.enabled = true;
        }

        yield return new WaitForSeconds(tempoinvisivel);

        // Desativa
        foreach (GameObject obj in invisivelObjects)
        {
            var renderer = obj.GetComponent<Renderer>();
            if (renderer != null) renderer.enabled = false;
        }

        // espera cooldown antes de poder apertar de novo
        yield return new WaitForSeconds(tempoCooldown);
        podeApertar = true;
    }
}