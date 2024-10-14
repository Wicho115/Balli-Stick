using UnityEngine;

namespace _Balli_Stick.Car
{
    public abstract class CarInput : MonoBehaviour
    {
        public delegate void OnCarMovedDel(float moveValue);
        public delegate void OnCarChangedSpeedDel(float speedMultiplier);
        
        public event OnCarMovedDel OnCarMoved;
        public event OnCarChangedSpeedDel OnCarChangedSpeed;
        
        private bool _isGameStarted;

        protected void _OnCarMoved(float moveValue)
        {
            if (!_isGameStarted) return;
            OnCarMoved?.Invoke(moveValue);  
        } 
        protected void _OnCarChangedSpeed(float speedMultiplier) => OnCarChangedSpeed?.Invoke(speedMultiplier);
        
        private void OnGameStarted() => _isGameStarted = true;

        private void OnGameEnded() => _isGameStarted = false;
        
        private void OnEnable()
        {
            GameManager.OnGameStarted += OnGameStarted;
            GameManager.OnGameEnded += OnGameEnded;
        }

        private void OnDisable()
        {
            GameManager.OnGameStarted -= OnGameStarted;
            GameManager.OnGameEnded -= OnGameEnded;
        }
        
    }
}
