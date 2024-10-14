using System;
using System.Collections;
using System.Collections.Generic;
using _Balli_Stick;
using TMPro;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text gameOverText;
    [SerializeField] private GameObject panel;
    private void OnGameEnded()
    {
        panel.SetActive(true);
        gameOverText.text = GameManager.HasLost ? "Perdiste" : "Ganaste";
    }
    
    private void OnEnable()
    {
        GameManager.OnGameEnded += OnGameEnded;
    }

    private void OnDisable()
    {
        GameManager.OnGameEnded -= OnGameEnded;
    }

}
