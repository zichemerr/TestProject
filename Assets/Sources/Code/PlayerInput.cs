using System;
using UnityEngine;

namespace Sources.Code
{
    public class PlayerInput : MonoBehaviour
    {
        public float Horizontal => Input.GetAxis("Horizontal");

        public event Action Jumped;

        private void Update()
        {
            if (Input.GetKey(KeyCode.Space))
                Jumped?.Invoke();
        }
    }
}