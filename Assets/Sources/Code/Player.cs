using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Code
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerInput _input;
        [SerializeField] private int _startHealth = 2;
        
        private int _currentHealth;
        
        private void Start()
        {
            _currentHealth = _startHealth;
            Debug.Log("Current Health: " + _currentHealth);
            _movement.Init(_input);
        }

#if (UNITY_EDITOR)
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.R))
                RestartScene();
        }
#endif
        private void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void TakeDamage(int damage)
        {
            _currentHealth -= damage;
            Debug.Log("Current Health: " + _currentHealth);

            if (_currentHealth <= 0)
            {
                RestartScene();
            }
        }
    }
}
