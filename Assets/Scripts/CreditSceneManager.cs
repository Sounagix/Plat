using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditSceneManager : MonoBehaviour
{
    [SerializeField]
    private Button backButton;


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
}
