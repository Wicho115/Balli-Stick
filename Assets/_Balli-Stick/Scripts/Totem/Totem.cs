using UnityEngine;

namespace _Balli_Stick.Totem
{
    public class Totem : MonoBehaviour
    {
        public Transform spawn;

        private Vector3 _ballDir;
        private void Start()
        {
            _ballDir = spawn.forward.normalized;
            _ballDir.y = 0;
        }

        public void AppearBall(MetalBall ball)
        {
            ball.SetInitialConfig(_ballDir, spawn.position);
        }
    }
}
