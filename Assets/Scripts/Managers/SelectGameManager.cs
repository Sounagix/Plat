using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectGameManager : MonoBehaviour
{
    [SerializeField]
    private Button firstLevelButton;

    [SerializeField]
    private Button secondLevelButton;

    [SerializeField]
    private Button thirdLevelButton;

    [SerializeField]
    private Button backButton;

    private void Awake()
    {
        SetUpButtons();
    }

    private void SetUpButtons()
    {
        int currentLevelsActived = GameManager.Instance.GetCurrentLevelsActive();

        if (currentLevelsActived <= 1)
        {
            firstLevelButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.ChangeScene(4);
            });
        }

        if (currentLevelsActived >= 2)
        {
            secondLevelButton.GetComponent<Image>().color = Color.white;
            secondLevelButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.ChangeScene(5);
            });
        }

        if (currentLevelsActived >= 3)
        {
            thirdLevelButton.GetComponent<Image>().color = Color.white;
            thirdLevelButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.ChangeScene(6);
            });
        }

        backButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.ChangeScene(0);
            });
    }
}
