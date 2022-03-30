using System;
using System.Collections;
using System.Collections.Generic;
using Code.AssetManagement;
using Code.Infrastructure;
using Code.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Core
{
    public class PlayerControlService : IPlayerControlService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;
        private readonly ICoroutineRunner _coroutineRunner;

        private readonly Queue<Vector3> _path = new Queue<Vector3>();
        private Player _player;
        private Vector3 _targetPosition;
        private Vector3 _spawn;
        private Quaternion _targetRotation;

        private float _moveOffset;
        private Coroutine _swipe;
        private Vector3 _currentTarget;

        public PlayerControlService(IAssetProvider assetProvider, IStaticDataService staticDataService, ICoroutineRunner coroutineRunner)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
            _coroutineRunner = coroutineRunner;
        }

        public event Action PathPassed;

        private float SwipeDistance => _staticDataService.Data.Spread * _staticDataService.Data.SwipePercentage / 2;
        private float DistanceToTarget => (_targetPosition - _player.transform.position).magnitude;
        private bool TargetReached => DistanceToTarget < 0.001f;

        public void SpawnPlayer(BrokenLine brokenLine)
        {
            CreatePath(brokenLine);
            _spawn = _path.Dequeue();
            _player = Object.Instantiate(_assetProvider.Load<Player>(AssetPaths.PlayerPath), _spawn, Quaternion.identity);
            _targetPosition = _path.Dequeue();
            GetNextTarget();
        }

        public void Move()
        {
            if(TargetReached)
                GetNextTarget();

            if(_swipe != null) return;
            _player.transform.position = Vector3.MoveTowards(_player.transform.position, _targetPosition, Time.deltaTime * _staticDataService.Data.MoveSpeed);
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRotation, Time.deltaTime * _staticDataService.Data.RotateSpeed);
        }

        public void SwipeLeft()
        {
            if(_moveOffset > -SwipeDistance)
            {
                if(_swipe == null)
                {
                    _moveOffset -= SwipeDistance;
                    CalculateTargetPosition();
                    _swipe = _coroutineRunner.StartCoroutine(Swipe());
                }
            }

        }

        public void SwipeRight()
        {
            if(_moveOffset < SwipeDistance)
            {
                if(_swipe == null)
                {
                    _moveOffset += SwipeDistance;
                    CalculateTargetPosition();
                    _swipe = _coroutineRunner.StartCoroutine(Swipe());
                }
            }

        }

        private void GetNextTarget()
        {
            if(_path.Count == 0)
            {
                PathPassed?.Invoke();
                return;
            }

            _currentTarget = _path.Dequeue();
            CalculateTargetPosition();
            _targetRotation = Quaternion.LookRotation(_targetPosition - _player.transform.position);
        }

        private void CalculateTargetPosition() =>
            _targetPosition = new Vector3(_currentTarget.x + _moveOffset, _currentTarget.y, _currentTarget.z);

        private void CreatePath(BrokenLine brokenLine)
        {
            foreach (Vector3 point in brokenLine.Points)
                _path.Enqueue(point);
        }

        private IEnumerator Swipe()
        {
            float swipeTime = 0;
            Vector3 start = _player.transform.position;
            Vector3 endTarget = new Vector3(start.x + _moveOffset, start.y, start.z); 
            while (swipeTime < _staticDataService.Data.SwipeTime)
            {
                _player.transform.position = Vector3.Lerp(start, endTarget, swipeTime / _staticDataService.Data.SwipeTime);
                swipeTime += Time.deltaTime;
                yield return null;
            }

            _swipe = null;
        }
    }
}