using System;
using _Balli_Stick;
using TMPro;
using UnityEngine;

public class PointUI : MonoBehaviour
{
    [SerializeField] private TMP_Text pointText;
    [SerializeField] private HealthSystem healthSystem;

    private void Start() => pointText.text = healthSystem.InitialHealth.ToString();
    private void OnDamage(int newhealth) => pointText.text = newhealth.ToString();
    private void OnEnable() => healthSystem.OnDamage += OnDamage;
    private void OnDisable() => healthSystem.OnDamage -= OnDamage;


}
