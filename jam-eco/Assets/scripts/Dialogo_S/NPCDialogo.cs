using System.Collections;
using UnityEngine;
using TMPro;

public class NPCDialogo : MonoBehaviour
{
    [System.Serializable]
    public class LinhaDialogo
    {
        public string nome;
        [TextArea(2, 5)] public string texto;
    }

    [Header("Referências")]
    public GameObject dialogoBox;
    public TMP_Text dialogoText;
    public TMP_Text nomeText; 

    [Header("Diálogo")]
    public LinhaDialogo[] dialogo;

    [Header("Configuração")]
    public float wordSpeed = 0.05f;
    public float waitTime = 2f;

    int index = 0;
    bool playerISClose = false;
    bool dialogoAtivo = false;
    Coroutine currentCoroutine = null;

    void Start()
    {
        // Garante que a caixa de diálogo comece invisível
        if (dialogoBox != null)
        {
            dialogoBox.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && playerISClose && !dialogoAtivo)
        {
            StartDialog();
        }
    }

    void StartDialog()
    {
        if (dialogo.Length == 0 || dialogoBox == null) return;

        dialogoBox.SetActive(true);
        dialogoAtivo = true;
        index = 0;

        currentCoroutine = StartCoroutine(ShowDialogo());
    }

    IEnumerator ShowDialogo()
    {
        while (index < dialogo.Length)
        {
            nomeText.text = dialogo[index].nome;
            dialogoText.text = "";

            foreach (char c in dialogo[index].texto.ToCharArray())
            {
                dialogoText.text += c;
                yield return new WaitForSeconds(wordSpeed);
            }

            yield return new WaitForSeconds(waitTime);
            index++;
        }

        EndDialog();
    }

    public void EndDialog()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
            currentCoroutine = null;
        }

        dialogoAtivo = false;

        if (dialogoBox != null) dialogoBox.SetActive(false);
        if (nomeText != null) nomeText.text = "";
        if (dialogoText != null) dialogoText.text = "";

        index = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerISClose = true;
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerISClose = false;
            EndDialog();
        }
    }
}
