using UnityEngine;

namespace Hockey
{
    [CreateAssetMenu(menuName = "Hockey/Hockey Settings", fileName = "HockeySettings")]
    public class HockeySettings : ScriptableObject
    {
        [SerializeField] private float _maxDragDistance = 4f;
        [SerializeField] private float _maxImpulse = 20f;

        [SerializeField] private LayerMask _wallMask;

        [SerializeField] private int _maxWallReflections = 2;
        [SerializeField] private float _wallBounceMultiplier = 0.95f;

        [SerializeField] private int _trajectoryReflections = 3;

        [SerializeField] private float _trajectoryDistancePerImpulse = 0.8f;
        [SerializeField] private float _maxTrajectoryDistance = 20f;
        [SerializeField] private float _raycastStartOffset = 0.03f;
        
        public float MaxDragDistance => _maxDragDistance;
        public float MaxImpulse => _maxImpulse;

        public LayerMask WallMask => _wallMask;
        public int MaxWallReflections => _maxWallReflections;
        public float WallBounceMultiplier => _wallBounceMultiplier;

        public int TrajectoryReflections => _trajectoryReflections;
        public float TrajectoryDistancePerImpulse => _trajectoryDistancePerImpulse;
        public float MaxTrajectoryDistance => _maxTrajectoryDistance;
        public float RaycastStartOffset => _raycastStartOffset;
    }
}