
using _Balli_Stick.Miscellaneous;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace _Balli_Stick
{
    public class MetalBall : MonoBehaviour
    {
        public static event System.Action<MetalBall> OnMetalLifeHit;
        
        [HideInInspector] public int id;
        [SerializeField] private float speed;
        [SerializeField] private Range angleRange;

        private bool _firstHit;
        private Vector3 _dir;

        public void SetInitialConfig(Vector3 newDir, Vector3 newPos)
        {
            transform.position = newPos;
            var rot = Quaternion.AngleAxis(Random.Range(angleRange.Min, angleRange.Max), Vector3.up);
            ChangeDirection(rot * newDir);
        }

        private void Update()
        {
            transform.position += speed * Time.deltaTime * _dir;
        }

        private void ChangeDirection(Vector3 direccionNueva)
        {
            _dir = direccionNueva;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Vida"))
            {
                other.gameObject.GetComponent<HealthSystem>().Damage();
                OnMetalLifeHit?.Invoke(this);
                return;
            }
        
        
            if (other.transform.CompareTag("Pared"))
            {
                if (!_firstHit)
                {
                    _firstHit = true;
                    return;
                }
            
                var pos1 = transform.position;
                var pos2 = other.transform.position;
                pos1.y = pos2.y = 0;

                var normal = (pos2 - pos1).normalized;

                var newDir = (Vector3.Reflect(pos1, normal)).normalized;

                ChangeDirection(newDir);
            }
        }
    }

    
}
