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


    private void Awake()
    {
        SetUpButtons();
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
