using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegisterLifeText : MonoBehaviour
{
    private void Start()
    {
        TextMeshProUGUI text=GetComponent<TextMeshProUGUI>();
        GameManager.Instance.RegisterLifeText(text);
    }
}
