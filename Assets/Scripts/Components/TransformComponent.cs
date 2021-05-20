using UnityEngine;

namespace Components
{
    public class TransformComponent : MonoBehaviour
    {
        public Vector2 Direction { get; set; }
        
        public void Start()
        {
            TryGetComponent<Rigidbody2D>(out rigidBody);
        }
        
        public void FixedUpdate()
        {
            if (rigidBody != null)
                rigidBody.velocity = Direction.normalized * accelerate;
        }

        private readonly float accelerate = 3f;
        private Rigidbody2D rigidBody;
        private GameObject bulletPrefab;
    }
}