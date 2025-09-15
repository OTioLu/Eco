using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum State
{
    disable,
    typing,
    waiting
}

public class DialogueSystem : MonoBehaviour
{
    public DalogueData DalogueData;      // ScriptableObject com fal falas
    public GameObject dialogueUI;        // painel da UI
    public TypeTextAnimation typeText;   // script de texto digitado
    public TextMeshProUGUI nameText;     // ?? arraste o TMP do nome aqui
    public int currentIndex = 0;

    private State state = State.disable;

    void Awake()
    {
        if (typeText == null)
            typeText = FindObjectOfType<TypeTextAnimation>();
    }

    void Start()
    {
        state = State.disable;
        if (dialogueUI != null) dialogueUI.SetActive(false);
    }

    public void Interact()
    {
        if (DalogueData == null || DalogueData.talkScript == null || DalogueData.talkScript.Count == 0)
            return;

        if (state == State.disable)
        {
            StartDialogue();
        }
        else if (state == State.typing)
        {
            typeText.FinishTypingInstant();
            state = State.waiting;
        }
        else if (state == State.waiting)
        {
            Next();
        }
    }

    void StartDialogue()
    {
        currentIndex = 0;
        if (dialogueUI != null) dialogueUI.SetActive(true);
        ShowCurrentDialogue();
    }

    void ShowCurrentDialogue()
    {
        if (currentIndex < 0 || currentIndex >= DalogueData.talkScript.Count)
        {
            EndDialogue();
            return;
        }

        var entry = DalogueData.talkScript[currentIndex];

        // ?? Atualiza o campo do nome
        if (nameText != null)
            nameText.text = entry.name;

        if (typeText == null)
        {
            Debug.LogWarning("TypeTextAnimation não atribuído no DialogueSystem.");
            EndDialogue();
            return;
        }

        typeText.fullText = entry.text;
        state = State.typing;

        typeText.StartTyping(() =>
        {
            state = State.waiting;
        });
    }

    void Next()
    {
        if (state != State.waiting) return;

        currentIndex++;
        if (currentIndex < DalogueData.talkScript.Count)
        {
            ShowCurrentDialogue();
        }
        else
        {
            EndDialogue();
        }
    }

    void EndDialogue()
    {
        state = State.disable;
        if (dialogueUI != null) dialogueUI.SetActive(false);
    }

    public bool IsOpen()
    {
        return state != State.disable;
    }
}