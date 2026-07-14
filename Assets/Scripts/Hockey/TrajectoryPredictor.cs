using System.Collections.Generic;
using UnityEngine;

namespace Hockey
{
    public class TrajectoryPredictor
    {
        private readonly HockeySettings _settings;

        public TrajectoryPredictor(HockeySettings settings)
        {
            _settings = settings;
        }

        public Vector3[] Predict(Vector3 startPosition, Vector3 impulse)
        {
            if (impulse.sqrMagnitude <= 0.001f)
                return new[] { startPosition };

            var points = new List<Vector3>();
            points.Add(startPosition);

            var direction = impulse.normalized;

            var distance = impulse.magnitude * _settings.TrajectoryDistancePerImpulse;
            distance = Mathf.Min(distance, _settings.MaxTrajectoryDistance);

            var currentPosition = startPosition;
            var remainingDistance = distance;

            for (int i = 0; i <= _settings.TrajectoryReflections; i++)
            {
                if (remainingDistance <= 0f)
                    break;

                var hasHit = Physics.Raycast(currentPosition, direction, out RaycastHit hit, remainingDistance, _settings.WallMask);

                if (!hasHit)
                {
                    points.Add(currentPosition + direction * remainingDistance);
                    break;
                }

                points.Add(hit.point);

                remainingDistance -= hit.distance;

                var normal = hit.normal;
                normal.y = 0f;
                normal.Normalize();

                direction = Vector3.Reflect(direction, normal).normalized;

                currentPosition = hit.point + normal * _settings.RaycastStartOffset;
            }

            return points.ToArray();
        }
    }
}