using System;
using UnityEngine;

namespace Sources.Code
{
    public class PlayerInput : MonoBehaviour
    {
        public float Horizontal => Input.GetAxis("Horizontal");
        public float Vertical => Input.GetAxis("Vertical");

        public bool IsActive { get; private set; } = false;

        public event Action Jumped;

        public void Init()
        {
            Enable();
        }
        
        private void Update()
        {
            if (IsActive == false)
                return;
            
            if (Input.GetKey(KeyCode.Space))
                Jumped?.Invoke();
        }

        public void Enable()
        {
            IsActive = true;
        }
        
        public void Disable()
        {
            IsActive = false;
        }
    }
}