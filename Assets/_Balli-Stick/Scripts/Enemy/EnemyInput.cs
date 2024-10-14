using System;
using _Balli_Stick.Car;
using UnityEngine;
using Random = UnityEngine.Random;
using Range = _Balli_Stick.Miscellaneous.Range;

namespace _Balli_Stick.Enemy
{
    public class EnemyInput : CarInput
    {
        [SerializeField] private Range xRange;
        [SerializeField] private float changeDirectionTime = 2f;
        [SerializeField] private float restTime = 1f;
    
        private float _targetX; 
        private float _timer; 
        private float _restTimer; 
        private bool _isResting;
    
        void Start() =>SetNewTarget();

        void Update()
        {
            if (!_isResting)
                CalculateMove();
            else
                Rest();
        }

        private void Rest()
        {
            _restTimer -= Time.deltaTime;
            if (!(_restTimer <= 0f)) return;
        
            _isResting = false;
            SetNewTarget();
        }

        private void CalculateMove()
        {
            OnMove();

            _timer += Time.deltaTime;
            if (_timer >= changeDirectionTime)
            {
                SetNewTarget();
                _timer = 0f;
            }

            //No ha llegado, por lo tanto sigue moviendose
            if (!(Mathf.Abs(transform.position.x - _targetX) < 0.1f)) return;
        
            _isResting = true;
            _restTimer = restTime;
        }

        void SetNewTarget()
        {
            //todo: Hacer que esta posicion ubique a la pelota
            _targetX = Random.Range(xRange.Min, xRange.Max);
        }
    
        void OnMove()
        {
            _OnCarMoved(Mathf.Sign(_targetX));
        }
    }
}
