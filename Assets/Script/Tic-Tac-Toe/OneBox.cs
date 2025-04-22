using System;
using UnityEngine;
using UnityEngine.UI;

public class OneBox : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private int myIndex;

    public static event Action<int, Button> OnBoxClicked;

    void Start()
    {
        if (button == null)
        {
            button = GetComponent<Button>();
        }
        
        if (button != null)
        {
            button.onClick.AddListener(OnClick);
        }
    }

    void OnDestroy()
    {
        if (button != null)
        {
            button.onClick.RemoveListener(OnClick);
        }
    }

    private void OnClick()
    {
        OnBoxClicked?.Invoke(myIndex, button);
    }

    public Button GetButton()
    {
        return button;
    }
}