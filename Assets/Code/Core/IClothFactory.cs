using UnityEngine;

namespace Code.Core
{
    public interface IClothFactory
    {
        Cloth Create(string name, Mesh mesh, BrokenLine brokenLine);
    }
}