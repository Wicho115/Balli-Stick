using UnityEngine;

namespace _Balli_Stick
{
    public class HealthSystem : MonoBehaviour
    {
        public delegate void OnDamageDel(int newHealth);
        public delegate void OnDeathDel();
    
        public event OnDamageDel OnDamage;
        public event OnDeathDel OnDeath;

        private int _currentHealth;
        public int CurrentHealth
        {
            get => _currentHealth;
        
            private set
            {
                if (_currentHealth > value)
                {
                    OnDamage?.Invoke(value);
                }
                _currentHealth = value;

                if (_currentHealth <= 0)
                {
                    Death();
                }
            }
        }

        public int InitialHealth => initialHealth;
        [SerializeField] private int initialHealth;
        
        private void Start()
        {
            _currentHealth = initialHealth;
        }

        public void Damage()
        {
            CurrentHealth--;
        }
    
        private void Death()
        {
            OnDeath?.Invoke();
        }
    }
}
