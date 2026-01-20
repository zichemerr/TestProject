using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sources.Code
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerUI playerUIPrefab;
        [SerializeField] private int _startHealth = 2;

        private PlayerUI _playerUI;
        private int _currentHealth;
        
        private void Start()
        {
            _playerUI = Instantiate(playerUIPrefab);
            _playerUI.Init();
            
            _playerUI.ExitButtonPressed += OnExitButtonPressed;
            _playerUI.RestartButtonPressed += OnRestartButtonPressed;
            
            _currentHealth = _startHealth;
            
            _input.Init();
            _movement.Init(_input);
        }

        private void OnDisable()
        {
            _playerUI.ExitButtonPressed -= OnExitButtonPressed;
            _playerUI.RestartButtonPressed -= OnRestartButtonPressed;
        }
        
        private void OnExitButtonPressed()
        {
            Debug.Log("Exit Game");
        }
        
        private void OnRestartButtonPressed()
        {
            RestartScene();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Finish>())
            {
                _input.Disable();
                _movement.Disable();
                _playerUI.ShowWinMenu();
            }
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
