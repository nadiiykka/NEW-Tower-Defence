using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CutsceneManager : MonoBehaviour
{
    public TextMeshProUGUI myText;
    public string[] textLines; 
    private int currentLine = 0; 
    private bool isLastLine => currentLine >= textLines.Length - 1;

    void Start()
    {
        if (textLines.Length > 0)
        {
            myText.text = textLines[0];
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isLastLine)
            {
                SceneManager.LoadScene("Game");
            }
            else
            { 
                currentLine++;
                myText.text = textLines[currentLine];
            }
        }
    }
}
