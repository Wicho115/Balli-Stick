using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Balli_Stick.Totem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace _Balli_Stick
{
    public class GameManager : MonoBehaviour
    {
        public static event Action OnGameStarted;
        public static event Action OnGameEnded;
        
        [SerializeField] private int segundosInicio;
        [SerializeField] private TotemManager totemManager;
        [SerializeField] private TMPro.TMP_Text text;

        public static bool HasLost;

        private List<Car.Car> _cars;
        
        private IEnumerator Start()
        {
            _cars = FindObjectsByType<Car.Car>(FindObjectsInactive.Include, FindObjectsSortMode.InstanceID).ToList();
            yield return new WaitForSeconds(.5f);
            
            for (int i = segundosInicio; i >= 0; i--)
            {
                text.text = i.ToString();
            
                yield return new WaitForSeconds(1);
            }

            text.text = "GO";
            yield return new WaitForSeconds(.75f);
            text.text = string.Empty;
        
            OnGameStarted?.Invoke();
            totemManager.Activate();
        }

        private void GameEnd()
        {
            OnGameEnded?.Invoke();
            totemManager.Deactivate();
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }
        
        private void OnCarDeath(Car.Car car)
        {
            if (!_cars.Contains(car)) return;
            _cars.Remove(car);
            
            if (car.CompareTag("Player"))
            {
                //Se murio el jugador, hacer algo
                HasLost = true;
                GameEnd();
                return;
            }

            if (_cars.Count <= 1)
            {
                HasLost = false;
                GameEnd();
            }
        }

        private void OnEnable() => Car.Car.OnCarDeath += OnCarDeath;
        private void OnDisable() => Car.Car.OnCarDeath -= OnCarDeath;
    }
}
