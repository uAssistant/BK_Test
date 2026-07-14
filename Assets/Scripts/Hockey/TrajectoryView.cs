using UnityEngine;

namespace Hockey
{
    public class TrajectoryView : MonoBehaviour
    {
        [SerializeField] private LineRenderer _trajectoryLine;
        
        [SerializeField] private LineRenderer _forceLine;
        [SerializeField] private float _forceLineLengthMultiplier = 0.35f;
        [SerializeField] private Color _lowPowerColor = Color.green;
        [SerializeField] private Color _highPowerColor = Color.red;

        public void Show(Vector3[] points, Vector3 impulse, float normalizedPower)
        {
            if (points is { Length: >= 2 })
            {
                _trajectoryLine.enabled = true;
                _trajectoryLine.positionCount = points.Length;
                for (int i = 0; i < points.Length; i++)
                    _trajectoryLine.SetPosition(i, points[i]);
            }
            else
            {
                _trajectoryLine.enabled = false;
            }
            
            if (points is { Length: >= 1 } && impulse.sqrMagnitude > 0.001f)
            {
                var powerColor = Color.Lerp(_lowPowerColor, _highPowerColor, normalizedPower);
                
                var direction = impulse.normalized;
                direction.y = 0f;
                direction.Normalize();

                var endPosition = points[0] + direction * impulse.magnitude * _forceLineLengthMultiplier;

                _forceLine.enabled = true;
                
                _forceLine.startColor = powerColor;
                _forceLine.endColor = powerColor;
                
                _forceLine.positionCount = 2;
                _forceLine.SetPosition(0, points[0]);
                _forceLine.SetPosition(1, endPosition);
            }
            else
            {
                _forceLine.enabled = false;
            }
        }

        public void Hide()
        {
            _trajectoryLine.enabled = false;
            _forceLine.enabled = false;
        }
    }
}