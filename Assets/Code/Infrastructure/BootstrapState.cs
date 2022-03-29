using System;
using Code.Core;

namespace Code.Infrastructure
{
    public class BootstrapState : IState
    {
        public RectMeshGenerator RectMeshGenerator;
        public MeshCutter MeshCutter;
        
        
        
        public void Enter()
        {
            throw new NotImplementedException();
        }
    }
}