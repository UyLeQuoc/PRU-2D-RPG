using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[System.Serializable]
public class QuestionData
{
    public QtsData.Question[] array; // Đặt tên mảng là "array"
}

public class QuestionManage : MonoBehaviour
{
    [SerializeField] private Canvas questionCanvas;

    public TextMeshProUGUI questionText;
    public TextMeshProUGUI scoreText;

    public TextMeshProUGUI finalScore;

    public Button[] replyButtons; // nút đáp án, có 4 nút
    public GameObject reviseButton;// nút hồi sinh

    public QtsData qtsData;

    public GameObject Right;
    public GameObject Wrong;
    public GameObject GameFinished;

    private int currentQuestion = 0;
    private static int score = 0;
    private static int totalExp = 0;

    private void Start()
    {
        reviseButton.gameObject.SetActive(false);
        scoreText.text = $"Your score is: {score} ";
        Right.gameObject.SetActive(false);
        Wrong.gameObject.SetActive(false);
        GameFinished.gameObject.SetActive(false);
        LoadFromjson();
        // Thiết lập câu hỏi đầu tiên
        SetQuestion(currentQuestion);
    }

    private void LoadFromjson()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "questions.json");

        if (!File.Exists(filePath))
        {
            Debug.LogError("Questions file not found at: " + filePath);
            return; // Hoặc xử lý lỗi theo cách khác
        }
        // Đọc nội dung file JSON
        string json = File.ReadAllText(filePath);

        // Chuyển đổi JSON thành mảng Question (giả sử bạn có lớp Question tương ứng)
        QuestionData questionData = JsonUtility.FromJson<QuestionData>(json);

        // Gán mảng câu hỏi vào Scriptable Object
        qtsData.questions = questionData.array; //questionData.array.OrderBy(x => Random.value).Take(10).ToArray();
    }

    private void SetQuestion(int questionIndex)
    {
        //questionText.text = qtsData.questions[questionIndex].questionText;

        ////remove previous listner before adding new ones
        //foreach (var button in replyButtons)
        //{
        //    button.onClick.RemoveAllListeners();
        //}

        //for (int i = 0; i < replyButtons.Length; i++)
        //{
        //    replyButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = qtsData.questions[questionIndex].replies[i];
        //    int replyIndex = i;
        //    replyButtons[i].onClick.AddListener(
        //        () =>
        //        {
        //            CheckReply(replyIndex);
        //        });
        //}

        //====  CODE Ở TRÊN CHƯA CÓ RANDOM
        questionText.text = qtsData.questions[questionIndex].questionText;

        // Tạo danh sách chỉ số đáp án và xáo trộn
        List<int> answerIndices = new List<int> { 0, 1, 2, 3 };
        answerIndices = answerIndices.OrderBy(x => Random.value).ToList();

        for (int i = 0; i < replyButtons.Length; i++)
        {
            int shuffledIndex = answerIndices[i];
            replyButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = qtsData.questions[questionIndex].replies[shuffledIndex];

            // Lưu trữ thông tin về chỉ số gốc của đáp án
            replyButtons[i].onClick.RemoveAllListeners();
            replyButtons[i].onClick.AddListener(() => CheckReply(shuffledIndex));
        }
    }

    private void CheckReply(int replyIndex)
    {
        if (replyIndex == qtsData.questions[currentQuestion].correctReplyIndex)
        {
            score++;
            totalExp += 50; // trả lời đúng + 50 exp

            scoreText.text = $"Your score is: {score} ";

            //Enable right panel
            Right.gameObject.SetActive(true);

            foreach (var button in replyButtons)
            {
                button.interactable = false;
            }
            //Next question
            StartCoroutine(Next());
        }
        else
        {
            Wrong.gameObject.SetActive(true);

            var checkHealth = GameManager.Instance.DamagePlayerHealth(1); // trừ 1 hp và lấy tình trạng máu
            if (!checkHealth) // người chơi đã chết = false
            {
                new WaitForSeconds(2);
                finalScore.text = "Bro you have answer wrong many times and do better next time";

                GameFinished.SetActive(true);
                reviseButton.SetActive(true); // nút hồi sinh bật lên
                return;
            }

            foreach (var button in replyButtons)
            {
                button.interactable = false;
            }
            //Next question

            StartCoroutine(Next());
        }
    }

    private IEnumerator Next()
    {
        yield return new WaitForSeconds(2);

        currentQuestion++;

        if (currentQuestion < qtsData.questions.Length)
        {
            Reset();
        }
        else
        {
            GameFinished.SetActive(true);

            float scorePercentage = (float)score / qtsData.questions.Length * 100;

            finalScore.text = "You scored: " + scorePercentage.ToString("F0") + "%";
            GameManager.Instance.AddPlayerExp(totalExp);

            if (scorePercentage < 50)
            {
                finalScore.text += ". Game over";
            }
            else
            {
                finalScore.text += ". Good job bro \nCongrats";
            }
            finalScore.text += "you have gained more: " + totalExp + " exps";
        }
    }

    public void Reset()
    {
        Right.SetActive(false);
        Wrong.SetActive(false);

        foreach (var button in replyButtons)
        {
            button.interactable = true;
        }

        SetQuestion(currentQuestion);
    }

    public void Clear()
    {
        Right.SetActive(false);
        Wrong.SetActive(false);

        foreach (var button in replyButtons)
        {
            button.interactable = true;
        }
        currentQuestion = 0;
        score = 0;
        totalExp = 0;
        Start();
    }

    public void OnCloseButtonClick()
    {
        questionCanvas.enabled = false; // Ẩn Canvas "Question"
        Clear();
    }

    public void OnReviseButtonClick() // Khi người chơi trả lời thua và chết thì ấn nút này
    {
        GameManager.Instance.ResetPlayer();
        questionCanvas.enabled = false; // Ẩn Canvas "Question"
        Clear();
        SceneManager.LoadScene("SchoolScene");
    }
}