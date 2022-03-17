using UnityEngine;
using TMPro;
using System.Text;

public class Conversation : MonoBehaviour
{
    public TMP_InputField InputField;
    public GameObject SpeechBubble;
    public TMP_Text SpeechBubbleText;

    public void OnSubmitText()
    {
        int wordsCount = InputField.text.Split(' ').Length;
        string result = new StringBuilder().Insert(0, "Bark ", wordsCount).ToString();

        SpeechBubble.SetActive(true);
        SpeechBubbleText.SetText(result);

        InputField.text = "";        
    }
}