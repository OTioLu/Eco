using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TypeTextAnimation : MonoBehaviour
{
    public float TypeDelay = 0.05f;
    public TextMeshProUGUI textObject;
    public string fullText;

    private Action onFinishCallback;
    private Coroutine typingCoroutine;

    // Inicia a corrotina de digitação e registra callback
    public void StartTyping(Action onFinish = null)
    {
        StopTyping();
        onFinishCallback = onFinish;
        typingCoroutine = StartCoroutine(TypeTextCoroutine());
    }

    // Finaliza imediatamente a digitação (mostra todo o texto e chama o callback)
    public void FinishTypingInstant()
    {
        StopTyping();
        if (textObject != null)
        {
            textObject.text = fullText;
            textObject.maxVisibleCharacters = fullText != null ? fullText.Length : 0;
        }
        onFinishCallback?.Invoke();
        onFinishCallback = null;
    }

    private IEnumerator TypeTextCoroutine()
    {
        if (textObject == null)
        {
            onFinishCallback?.Invoke();
            onFinishCallback = null;
            yield break;
        }

        textObject.text = fullText ?? "";
        textObject.maxVisibleCharacters = 0;

        int len = textObject.text.Length;
        for (int i = 1; i <= len; i++)
        {
            textObject.maxVisibleCharacters = i;
            yield return new WaitForSeconds(TypeDelay);
        }

        typingCoroutine = null;
        onFinishCallback?.Invoke();
        onFinishCallback = null;
    }

    private void StopTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
            typingCoroutine = null;
        }
    }
}
