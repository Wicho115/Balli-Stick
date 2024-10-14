using UnityEngine;

using _Balli_Stick.Car;

namespace _Balli_Stick.Player
{
    public class PlayerInput : CarInput
    {
        private void Update()
        {
            var h = Input.GetAxis("Horizontal");
            _OnCarMoved(h);
        }
        
    }
}
