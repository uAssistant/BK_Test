using UnityEngine;
using Zenject;

namespace Hockey
{
    public class PuckBody : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _contactRadius;

        private HockeySettings _settings;

        private Vector3 _startPosition;
        private Quaternion _startRotation;
        private int _wallReflections;
        
        private Vector3 _lastVelocity;
    
        public Vector3 Position => _rigidbody.position;
        public bool IsMoving => _rigidbody.linearVelocity.sqrMagnitude > 0.01f;
        public float Radius => _contactRadius;

        [Inject]
        public void Init(HockeySettings settings)
        {
            _settings = settings;

            _startPosition = _rigidbody.position;
            _startRotation = _rigidbody.rotation;
        }

        public void ApplyImpulse(Vector3 impulse)
        {
            _wallReflections = 0;

            _rigidbody.AddForce(impulse, ForceMode.Impulse);
        }
        
        private void FixedUpdate()
        {
            _lastVelocity = _rigidbody.linearVelocity;
        }

        public void ResetToStart()
        {
            _wallReflections = 0;

            _rigidbody.linearVelocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;

            _rigidbody.position = _startPosition;
            _rigidbody.rotation = _startRotation;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (!IsInLayerMask(collision.gameObject.layer, _settings.WallMask))
                return;

            if (collision.contactCount == 0)
                return;
            
            var velocity = _lastVelocity;
            velocity.y = 0f;

            var normal = collision.GetContact(0).normal;
            normal.y = 0f;
            normal.Normalize();

            var reflectedVelocity = Vector3.Reflect(velocity, normal);
            reflectedVelocity *= _settings.WallBounceMultiplier;

            _rigidbody.linearVelocity = reflectedVelocity;

            _wallReflections++;

            if (_wallReflections > _settings.MaxWallReflections)
                ResetToStart();
        }

        private bool IsInLayerMask(int layer, LayerMask mask)
        {
            return (mask.value & (1 << layer)) != 0;
        }
    }
}