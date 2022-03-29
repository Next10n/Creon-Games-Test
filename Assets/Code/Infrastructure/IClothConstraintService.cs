using Code.Core;
using UnityEngine;

namespace Code.Infrastructure
{
    public interface IClothConstraintService
    {
        void ApplyConstraints(Cloth firstCloth, Cloth secondCloth, BrokenLine brokenLine);
    }
}