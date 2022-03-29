using Code.Infrastructure;
using Code.Infrastructure.StateMachine;
using UnityEngine;
using Zenject;

namespace Code.Core
{
    public class UpdateBehaviour : MonoBehaviour
    {
        private IUpdateService _updateService;
        
        [Inject]
        private void Construct(IUpdateService updateService)
        {
            _updateService = updateService;
        }

        private void Update() =>
            _updateService.Update();
    }
}