using System;
using System.Collections.Generic;
using Code.AssetManagement;
using Code.StaticData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Code.Core
{
    public class PlayerControlService : IPlayerControlService
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IStaticDataService _staticDataService;

        private readonly Queue<Vector3> _path = new Queue<Vector3>();
        private Player _player;
        private Vector3 _targetPosition;
        private Vector3 _spawn;
        private Quaternion _targetRotation;

        public PlayerControlService(IAssetProvider assetProvider, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _staticDataService = staticDataService;
        }

        public event Action PathPassed;

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

            _player.transform.position = Vector3.MoveTowards(_player.transform.position, _targetPosition, Time.deltaTime * _staticDataService.Data.MoveSpeed);
            _player.transform.rotation = Quaternion.RotateTowards(_player.transform.rotation, _targetRotation, Time.deltaTime * _staticDataService.Data.RotateSpeed);
        }

        private void GetNextTarget()
        {
            if(_path.Count == 0)
            {
                PathPassed?.Invoke();
                return;
            }

            _targetPosition = _path.Dequeue();
            _targetRotation = Quaternion.LookRotation(_targetPosition - _player.transform.position);
        }

        private void CreatePath(BrokenLine brokenLine)
        {
            foreach (Vector3 point in brokenLine.Points)
                _path.Enqueue(point);
        }

    }
}