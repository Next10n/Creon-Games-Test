using Code.StaticData;

namespace Code.Core
{
    public class RectMeshGenerator : IRectMeshGenerator
    {
        private readonly IStaticDataService _staticDataService;

        public RectMeshGenerator(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }
        
        public RectMesh CreateMesh() =>
            new RectMesh(_staticDataService.Data.Width, _staticDataService.Data.Height);
    }
}