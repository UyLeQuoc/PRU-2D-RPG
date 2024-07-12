using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenQuestionScript : MonoBehaviour
{
    public bool showDialog = false; // Biến cờ để kiểm soát hiển thị bảng thông báo

    [SerializeField] private Canvas questionCanvas; // Biến public để kéo thả Canvas từ Inspector

    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Kiểm tra xem có phải người chơi va chạm không
        {
            showDialog = true; // Hiển thị bảng thông báo khi người chơi va chạm
        }
    }

    private void OnGUI()
    {
        if (showDialog)
        {
            // Vẽ bảng thông báo với hai nút "Có" và "Không"
            GUI.Box(new Rect(Screen.width / 2 - 150, Screen.height / 2 - 50, 300, 100), "Bạn có muốn vào học không?");

            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2, 80, 30), "Có"))
            {
                questionCanvas.enabled = true; // Ẩn Canvas lúc đầu

                showDialog = false; // Ẩn bảng thông báo sau khi người chơi chọn
            }

            if (GUI.Button(new Rect(Screen.width / 2 + 20, Screen.height / 2, 80, 30), "Không"))
            {
                showDialog = false; // Ẩn bảng thông báo nếu người chơi chọn "Không"
            }
        }
    }

    public void OnCloseButtonClick()
    {
        questionCanvas.enabled = false; // Ẩn Canvas "Question"
    }
}