using Code.Core;
using Code.StaticData;
using UnityEngine;

namespace Code.Inputs
{
    public class MouseSwipePlayer : ISwipePlayerService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerControlService _playerControlService;

        private Vector3 _startSwipePosition;
        
        private MouseSwipePlayer(IStaticDataService staticDataService, IPlayerControlService playerControlService)
        {
            _staticDataService = staticDataService;
            _playerControlService = playerControlService;
        }
        
        public void DetectSwipe()
        {
            if(Input.GetMouseButtonDown(0))
                _startSwipePosition = Input.mousePosition;

            if(Input.GetMouseButtonUp(0))
                ReleaseSwipe(Input.mousePosition);
        }

        private void ReleaseSwipe(Vector3 endSwipePosition)
        {
            if(Vector3.Distance(_startSwipePosition, endSwipePosition) >= _staticDataService.Data.MinSwipe)
            {
                if(_startSwipePosition.x > endSwipePosition.x)
                    _playerControlService.SwipeLeft();
                else
                    _playerControlService.SwipeRight();

            }
        }
    }
}