using System;
using UnityEngine;

namespace Sources.Code
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField] private WinMenu _winMenu;
        
        public event Action RestartButtonPressed;
        public event Action ExitButtonPressed;
        
        public void Init()
        {
            _winMenu.Disable();
        }

        public void ShowWinMenu()
        {
            _winMenu.Enable();
        }

        public void OnExitButton()
        {
            ExitButtonPressed?.Invoke();
        }
        
        public void OnRestartButton()
        {
            RestartButtonPressed?.Invoke();
        }
    }
}