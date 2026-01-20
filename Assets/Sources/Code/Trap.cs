using UnityEngine;

namespace Sources.Code
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private int _damage = 1;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.TakeDamage(_damage);
                Destroy(gameObject);
            }
        }
    }
}
