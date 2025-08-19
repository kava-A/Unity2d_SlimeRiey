using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        UpdateCoinText();
    }

    public TextMeshProUGUI coinText;

    [Header("杀敌统计")]
    public int killCount = 0; // 杀敌数量
    public int coinCount = 0; // 金币数量

    public void AddKillCount(int count, int coin)
    {
        killCount += count;
        coinCount += coin;
        UpdateCoinText();
    }
    public void UseCoin(int coin)
    {
        coinCount -= coin;
        
        UpdateCoinText();
    }
    public void UpdateCoinText()
    {
        coinText.text = "金币: " + coinCount.ToString();
    }
}
