using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NameStorage : MonoBehaviour
{
    public static string playerName;
    public TMP_Text nameInput;
    public TMP_InputField nameField;

    private void Start()
    {
        nameField.text = playerName;
    }

    private void Update()
    {
        if (nameInput)
        {
            playerName = nameInput.text.ToUpper();
        }
    }
}
