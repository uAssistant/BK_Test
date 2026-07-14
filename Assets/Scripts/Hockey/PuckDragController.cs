using System;
using UnityEngine;
using Zenject;

namespace Hockey
{
    public class PuckDragController : IInitializable, IDisposable
    {
        private readonly PuckBody _puckBody;
        private readonly TrajectoryView _trajectoryView;
        private readonly TrajectoryPredictor _trajectoryPredictor;
        private readonly HockeySettings _settings;
        private readonly HockeyInputService _inputService;

        private bool _isDragging;
        private Vector3 _currentImpulse;

        public PuckDragController(PuckBody puckBody, TrajectoryView trajectoryView, TrajectoryPredictor trajectoryPredictor,
            HockeySettings settings, HockeyInputService inputService)
        {
            _puckBody = puckBody;
            _trajectoryView = trajectoryView;
            _trajectoryPredictor = trajectoryPredictor;
            _settings = settings;
            _inputService = inputService;
        }

        public void Initialize()
        {
            _trajectoryView.Hide();

            _inputService.PointerPressed += OnPointerPressed;
            _inputService.PointerMoved += OnPointerMoved;
            _inputService.PointerReleased += OnPointerReleased;
        }

        private void OnPointerPressed(Vector2 screenPosition)
        {
            if (_puckBody.IsMoving)
                return;
            
            var toPointer = GetWorldPosition(screenPosition) - _puckBody.Position;
            toPointer.y = 0f;

            if (toPointer.sqrMagnitude > _puckBody.Radius)
                return;

            _isDragging = true;

            UpdateDrag(screenPosition);
        }

        private void OnPointerMoved(Vector2 screenPosition)
        {
            if (!_isDragging)
                return;

            UpdateDrag(screenPosition);
        }

        private void OnPointerReleased(Vector2 screenPosition)
        {
            if (!_isDragging)
                return;

            _isDragging = false;
            
            _trajectoryView.Hide();

            _puckBody.ApplyImpulse(_currentImpulse);

            _currentImpulse = Vector3.zero;
        }

        private void UpdateDrag(Vector2 screenPosition)
        {
            var dragVector = GetWorldPosition(screenPosition) - _puckBody.Position;
            dragVector.y = 0f;

            _currentImpulse = CalculateImpulse(dragVector, out float normalizedPower);

            var trajectoryPoints = _trajectoryPredictor.Predict(_puckBody.Position, _currentImpulse);
            _trajectoryView.Show(trajectoryPoints, _currentImpulse, normalizedPower);
        }

        private Vector3 CalculateImpulse(Vector3 dragVector, out float normalizedPower)
        {
            var distance = dragVector.magnitude;
            
            normalizedPower = Mathf.Clamp01(distance / _settings.MaxDragDistance);

            var direction = -dragVector / distance;

            return direction * normalizedPower * _settings.MaxImpulse;
        }

        private Vector3 GetWorldPosition(Vector2 screenPosition)
        {
            var worldPosition = Camera.main!.ScreenToWorldPoint(
                new Vector3(screenPosition.x, screenPosition.y, Camera.main!.transform.position.y - _puckBody.Position.y)
            );
            
            worldPosition.y = _puckBody.Position.y;
            return worldPosition;
        }
        
        public void Dispose()
        {
            _inputService.PointerPressed -= OnPointerPressed;
            _inputService.PointerMoved -= OnPointerMoved;
            _inputService.PointerReleased -= OnPointerReleased;

            _trajectoryView.Hide();
        }
    }
}