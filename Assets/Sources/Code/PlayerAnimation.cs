using DG.Tweening;
using UnityEngine;

namespace Sources.Code
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _walkScale = 1.2f;
        [SerializeField] private float _jumpScale = 1.5f;
        
        private Vector3 _originalScale;
        private bool _isWalking;

        public void Init()
        {
            _originalScale = transform.localScale;
        }

        private void Update()
        {
            bool isMoving = _rigidbody.linearVelocity.magnitude > 0.1f;
            
            if (isMoving && !_isWalking)
                StartWalking();
            else if (!isMoving && _isWalking)
                StopWalking();
        }

        private void StartWalking()
        {
            _isWalking = true;
            transform.DOScaleY(_originalScale.y * _walkScale, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine);
        }

        private void StopWalking()
        {
            _isWalking = false;
            DOTween.Kill(transform);
            transform.DOScaleY(_originalScale.y, 0.2f);
        }

        public void PlayJump()
        {
            DOTween.Kill(transform);
            
            transform.DOScaleY(_originalScale.y * _jumpScale, 0.2f)
                .OnComplete(() => 
                    transform.DOScaleY(_originalScale.y, 0.2f)
                        .OnComplete(() => 
                        {
                            if (_rigidbody.linearVelocity.magnitude > 0.1f)
                                StartWalking();
                        }));
        }
    }
}