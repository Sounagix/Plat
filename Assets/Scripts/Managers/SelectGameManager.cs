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
        firstLevelButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.ChangeScene(4);
            });
        secondLevelButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.ChangeScene(5);
            });
        thirdLevelButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.ChangeScene(6);
            });
        backButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.ChangeScene(0);
            });
    }
}
