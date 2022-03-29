using Code.Core;
using Code.Extensions;
using Code.SceneManagement;
using Code.StaticData;
using UnityEngine;

namespace Code.Infrastructure.StateMachine
{
    public class LoadGameState : IState
    {
        private readonly IRectMeshGenerator _rectMeshGenerator;
        private readonly IMeshCutter _meshCutter;
        private readonly IClothFactory _clothFactory;
        private readonly IClothConstraintService _clothConstraintService;
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerControlService _playerControlService;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        private BrokenLine _brokenLine;
        
        public LoadGameState(   
            IRectMeshGenerator rectMeshGenerator,
            IMeshCutter meshCutter,
            IClothFactory clothFactory,
            IClothConstraintService clothConstraintService,
            IStaticDataService staticDataService,
            IPlayerControlService playerControlService,
            IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader)
        {
            _rectMeshGenerator = rectMeshGenerator;
            _meshCutter = meshCutter;
            _clothFactory = clothFactory;
            _clothConstraintService = clothConstraintService;
            _staticDataService = staticDataService;
            _playerControlService = playerControlService;
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }
        
        public void Enter()
        {
            _sceneLoader.Load("Game", OnLoaded);
        }

        private void OnLoaded()
        {
            CreateCloths();
            SpawnPlayer();
            EnterGameLoop();
        }
        
        private void CreateCloths()
        {
            RectMesh rectMesh = _rectMeshGenerator.CreateMesh();
            _meshCutter.Cut(rectMesh, out Mesh leftMesh, out Mesh rightMesh, out _brokenLine);
            Cloth leftCloth = _clothFactory.Create("Left Cloth", leftMesh, _brokenLine);
            Cloth rightCloth = _clothFactory.Create("Right Cloth", rightMesh, _brokenLine);
            Spread(leftCloth.transform, rightCloth.transform);
            _clothConstraintService.ApplyConstraints(leftCloth, rightCloth, _brokenLine);
        }

        private void SpawnPlayer() =>
            _playerControlService.SpawnPlayer(_brokenLine);

        private void EnterGameLoop()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }

        private void Spread(Transform transform1, Transform transform2)
        {
            transform1.AddX(_staticDataService.Data.Spread / -2f);
            transform2.AddX(_staticDataService.Data.Spread / 2f);
        }
    }
}