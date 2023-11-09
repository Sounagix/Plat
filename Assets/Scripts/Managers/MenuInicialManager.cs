using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuInicialManager : MonoBehaviour
{
    [SerializeField]
    private Button playButton;

    [SerializeField]
    private Button exitButton;

    [SerializeField]
    private Button creditButton;

    [SerializeField]
    private Button optionsButton;

    private void Awake()
    {
        SetUpButtons();
    }

    // Agregar los eventos a los botones
    private void SetUpButtons()
    {
        playButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.ChangeScene(1);
            });
        exitButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.CloseApp();
            });
        creditButton.onClick.AddListener(
            delegate ()
            {
                GameManager.Instance.ChangeScene(2);
            });
        optionsButton.onClick.AddListener(
             delegate ()
             {
                 GameManager.Instance.ChangeScene(3);
             });
    }
}
