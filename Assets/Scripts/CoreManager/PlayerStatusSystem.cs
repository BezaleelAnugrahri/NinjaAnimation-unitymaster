using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatusSystem : MonoBehaviour
{
    public Image hpBar;
    float fill = 1;

    private void Start()
    {
        hpBar.fillAmount = 1;
        EventSystem.instance.PlayerHealthStatus += PlayerHPHandler;
    }

    private void Update()
    {
        hpBar.fillAmount = fill;
    }

    private void OnDisable()
    {
        EventSystem.instance.PlayerHealthStatus -= PlayerHPHandler;
    }

    private void PlayerHPHandler(int hpAmount)
    {
        fill = (float)hpAmount / 100;
    }
}
