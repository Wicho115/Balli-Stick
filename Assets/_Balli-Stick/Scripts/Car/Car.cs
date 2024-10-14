using UnityEngine;
using UnityEngine.Serialization;

namespace _Balli_Stick.Car
{
    public class Car : MonoBehaviour
    {
        public static event System.Action<Car> OnCarDeath; 
        
        [Header("Input Params")]
        [SerializeField] protected Vector3 moveAxis;
        [SerializeField] protected CarInput input;
    
        [Header("Movement Params")]
        [SerializeField] protected float baseSpeed;
        [SerializeField] protected float speedMultiplier = 1;
        [SerializeField] protected float minMove, maxMove;
        
        [Header("Health & Death")]
        [SerializeField] private HealthSystem healthSystem;
        [SerializeField] private GameObject wallObject;
        [SerializeField] private Collider carCollider;

        public float CurrentMove => _currentMove;
    
        private float _currentMove;
        private Vector3 _initialPos;
        
        private bool _isDead;

        private void Start()
        {
            _initialPos = transform.position;
            wallObject.SetActive(false);
        }

        protected virtual void OnMove(float value)
        {
            if (_isDead) return;
            
            //Se multiplican primero los flotantes por rendimiento de multiplicar los vectores
            _currentMove += value * baseSpeed * Time.deltaTime * speedMultiplier;
            _currentMove = Mathf.Clamp(_currentMove, minMove, maxMove);

            Vector3 finalMove = _currentMove * moveAxis;        
        
            transform.position = finalMove + _initialPos;
        }

        protected virtual void OnSpeedChanged(float newSpeedMultiplier)
        {
            speedMultiplier = newSpeedMultiplier;
        }
        
        private void OnDeath()
        {
            _isDead = true;
            wallObject.SetActive(true);
            carCollider.enabled = false;
            OnCarDeath?.Invoke(this);
        }

        private void OnEnable()
        {
            input.OnCarMoved += OnMove;
            input.OnCarChangedSpeed += OnSpeedChanged;
            
            healthSystem.OnDeath += OnDeath;
        }


        private void OnDisable()
        {
            input.OnCarMoved -= OnMove;
            input.OnCarChangedSpeed -= OnSpeedChanged;
            
            healthSystem.OnDeath -= OnDeath;
        }
    }
}
