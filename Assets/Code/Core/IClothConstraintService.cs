using UnityEngine;

namespace Code.Core
{
    public interface IClothConstraintService
    {
        void ApplyConstraints(Cloth firstCloth, Cloth secondCloth, BrokenLine brokenLine);
    }
}