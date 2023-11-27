using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelSceneManager : MonoBehaviour
{
    [SerializeField]
    private Button backButton;

    [SerializeField]
    private TextMeshProUGUI debugText;

    [SerializeField]
    private string initMsg;

    // número de estrellas necesarias para completar el nivel
    private int numStarNeeded;

    private int currentNumStar = 0;


    private void Awake()
    {
        Physics.gravity = new Vector3(0.0f,-4.0f,0.0f);
        SetUpButtons();
    }

    private void Start()
    {
        ShowMsg(initMsg);
    }

    private void SetUpButtons()
    {
        backButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.ChangeScene(0);
            });
    }

    public void ShowMsg(string msg)
    {
        debugText.text = msg;
        CancelInvoke();
        Invoke(nameof(EraseMsg),3.0f);
    }

    public void EraseMsg()
    {
        debugText.text = "";
    }
}
