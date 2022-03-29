using Code.Core;
using UnityEngine;

namespace Code.Infrastructure
{
    public interface IClothFactory
    {
        Cloth Create(string name, Mesh mesh, BrokenLine brokenLine);
    }
}