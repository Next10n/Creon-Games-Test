using UnityEngine;

namespace Code.Core
{
    public interface IMeshCutter
    {
        void Cut(RectMesh rectMesh, out Mesh leftMesh, out Mesh rightMesh, out BrokenLine brokenLine);
    }
}