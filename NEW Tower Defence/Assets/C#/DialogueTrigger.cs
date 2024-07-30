using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueCharacter
{
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine
{
    public DialogueCharacter character;
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue initialDialogue;  // Перший діалог
    public Dialogue secondaryDialogue; // Діалог після waveNumber == 1
    public Dialogue thirdDialogue;

    private bool hasTriggeredInitialDialogue = false;
    private bool hasTriggeredSecondaryDialogue = false;
    private bool hasTriggeredThirdDialogue = false;

    void Start()
    {
        // Запустити перші 18 ліній діалогу при запуску гри
        DialogueManager.Instance.StartDialogue(initialDialogue, 0, 18);
    }

    public void CheckForNextDialogue()
    {
        // Перевірити, чи був запущений початковий діалог та чи стан гри рівний "next"
        if (!hasTriggeredInitialDialogue && Manager.Instance.currentState == gameStatus.next)
        {
            hasTriggeredInitialDialogue = true;
            DialogueManager.Instance.StartDialogue(initialDialogue, 18, initialDialogue.dialogueLines.Count);
        }

        // Запустити другий діалог, якщо стан гри рівний "next" після першої хвилі
        if (hasTriggeredInitialDialogue && !hasTriggeredSecondaryDialogue && Manager.Instance.currentState == gameStatus.next)
        {
            DialogueManager.Instance.StartDialogue(secondaryDialogue);
            hasTriggeredSecondaryDialogue = true;
        }
    }

    private void Update()
    {
        // Викликати перевірку для запуску наступного діалогу
        CheckForNextDialogue();
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(secondaryDialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerDialogue();
    }
}