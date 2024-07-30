using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI characterName;
    public TextMeshProUGUI dialogueArea;
    public GameObject dialogueBackground; // Новий фон для тексту
    public GameObject continueButton;
    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;

    public float typingSpeed = 0.02f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        lines = new Queue<DialogueLine>();
        HideDialogueUI();  // Спочатку приховати UI діалогу
    }

    public void StartDialogue(Dialogue dialogue)
    {
        ShowDialogueUI();
        isDialogueActive = true;

        lines.Clear();

        foreach (DialogueLine dialogueLine in dialogue.dialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void StartDialogue(Dialogue dialogue, int startLine, int endLine)
    {
        ShowDialogueUI();
        isDialogueActive = true;

        lines.Clear();

        for (int i = startLine; i < endLine && i < dialogue.dialogueLines.Count; i++)
        {
            lines.Enqueue(dialogue.dialogueLines[i]);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        if (currentLine.character != null)
        {
            characterIcon.sprite = currentLine.character.icon;
            characterName.text = currentLine.character.name;
        }
        else
        {
            characterIcon.sprite = null;
            characterName.text = "Unknown";
        }

        StopAllCoroutines();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        dialogueArea.text = "";
        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue()
    {
        isDialogueActive = false;
        HideDialogueUI();  // Приховати UI діалогу після закінчення
    }

    public void ShowDialogueUI()
    {
        characterIcon.gameObject.SetActive(true);
        characterName.gameObject.SetActive(true);
        dialogueArea.gameObject.SetActive(true);
        dialogueBackground.gameObject.SetActive(true); // Показати фон для тексту
        continueButton.gameObject.SetActive(true);
    }

    public void HideDialogueUI()
    {
        characterIcon.gameObject.SetActive(false);
        characterName.gameObject.SetActive(false);
        dialogueArea.gameObject.SetActive(false);
        dialogueBackground.gameObject.SetActive(false); // Приховати фон для тексту
        continueButton.gameObject.SetActive(false);
    }
}
