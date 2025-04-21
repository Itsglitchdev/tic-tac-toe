using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class OneBox : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private int myIndex;

    public static event Action<int, Button> OnBoxClicked;

    void Start()
    {
        button.onClick.AddListener(onClick);
    }

    void onClick()
    {
        OnBoxClicked?.Invoke(myIndex, button);
    }
}
