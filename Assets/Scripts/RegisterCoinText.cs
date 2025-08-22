using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RegisterCoinText : MonoBehaviour
{
    private void Start()
    {
        TextMeshProUGUI coinText = GetComponent<TextMeshProUGUI>();
        GameManager.Instance.RegisterCoin(coinText);
    }
}
