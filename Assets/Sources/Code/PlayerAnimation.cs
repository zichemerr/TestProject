using DG.Tweening;
using UnityEngine;

namespace Sources.Code
{
    public class PlayerAnimation : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _pulseScale = 1.2f;
        [SerializeField] private float _pulseDuration = 0.5f;
        [SerializeField] private float _jumpScale = 1.5f;
        [SerializeField] private float _jumpDuration = 0.4f;
        [SerializeField] private Transform _transform;

        private int _pulseLoops = -1;
        private Ease _easeType = Ease.InOutSine;
        private Tween _walkTween;
        private Tween _jumpTween;
        private Vector3 _originalScale;

        private bool _isWalking;
        private bool _isJumping;

        public void Init()
        {
            _originalScale = _transform.localScale;
        }
        
        private void OnDestroy()
        {
            _walkTween?.Kill();
            _jumpTween?.Kill();
        }
        
        private void Update()
        {
            if (_isWalking == false && _rigidbody.linearVelocity.magnitude > 0.1f)
            {
                PlayWalk();
            }
            
            if (_rigidbody.linearVelocity.magnitude <= 0.1f && _isWalking)
            {
                StopWalk();
            }
        }

        private void PlayWalk()
        {
            if (_isWalking || _isJumping) return;

            _isWalking = true;
            
            _walkTween = _transform.DOScaleY(_originalScale.y * _pulseScale, _pulseDuration)
                .SetEase(_easeType)
                .SetLoops(_pulseLoops, LoopType.Yoyo)
                .SetLink(gameObject);
        }

        private void StopWalk()
        {
            if (!_isWalking || _walkTween == null) return;

            _isWalking = false;
            _walkTween.Kill();
            _walkTween = null;
            
            _transform.DOScaleY(_originalScale.y, 0.5f)
                .SetLink(gameObject);
        }
        
        public void PlayJump()
        {
            if (_isJumping)
                return;

            _isJumping = true;
            
            bool wasWalking = _isWalking;
            if (wasWalking)
            {
                StopWalk();
            }
            
            _jumpTween = _transform.DOScaleY(_originalScale.y * _jumpScale, _jumpDuration / 2f)
                .SetEase(Ease.OutQuad)
                .OnComplete(() =>
                {
                    _transform.DOScaleY(_originalScale.y, _jumpDuration / 2f)
                        .SetEase(Ease.InQuad)
                        .OnComplete(() =>
                        {
                            _isJumping = false;
                            _jumpTween = null;
                            
                            if (wasWalking && _rigidbody.linearVelocity.magnitude > 0.1f)
                            {
                                PlayWalk();
                            }
                        })
                        .SetLink(gameObject);
                })
                .SetLink(gameObject);
        }
    }
}