using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class QuestionManager : MonoBehaviour
{
    [SerializeField] private Canvas questionCanvas; // Biến public để kéo thả Canvas từ Inspector

    //public TextMeshProUGUI questionText;

    //public TextMeshProUGUI messageText;

    //public TMP_InputField inputField;

    //public string questionFrame;

    //private string[] elementsName;
    //private string[] answerValues;
    //private string correctAnswer;

    //private void Start()
    //{
    //    questionText = GetComponent<TextMeshProUGUI>();

    //    Load();
    //    GenerateQuestion();
    //}

    //private void Update()
    //{
    //}

    //public void GenerateQuestion()
    //{
    //    try
    //    {
    //        int i = UnityEngine.Random.Range(0, elementsName.Length);
    //        questionText.text = questionFrame + elementsName[i];
    //        correctAnswer = answerValues[i];

    //        inputField.text = "";
    //    }
    //    catch (System.Exception)
    //    {
    //        throw;
    //    }
    //}

    //public void submitAnswer()
    //{
    //    if (inputField.text == correctAnswer)
    //    {
    //        messageText.text = "Awesome you are correct!";
    //        Invoke("ClearMessage", 2f); // Ẩn message sau 2 giây
    //    }
    //    else
    //    {
    //        messageText.text = "Oh no u suck!";
    //        Invoke("ClearMessage", 3f); // Ẩn message sau 3 giây
    //    }
    //}

    //private void ClearMessage()
    //{
    //    messageText.text = "";
    //}

    //public void Load()
    //{
    private string filePath = Path.Combine(Application.streamingAssetsPath, "questions.json");

    //    if (File.Exists(filePath))
    //    {
    //        string jsonText = File.ReadAllText(filePath);
    //        QuestionData questionData = JsonUtility.FromJson<QuestionData>(jsonText);
    //        elementsName = questionData.elementsName;
    //        answerValues = questionData.answerValues;
    //    }
    //    else
    //    {
    //        Debug.LogError("File questions.json không tồn tại!");
    //    }
    //}

    //[System.Serializable]
    //public class QuestionData
    //{
    //    public string[] elementsName;
    //    public string[] answerValues;
    //}

    public void OnCloseButtonClick()
    {
        questionCanvas.enabled = false; // Ẩn Canvas "Question"
    }
}