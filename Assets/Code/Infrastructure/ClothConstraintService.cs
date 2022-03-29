using Code.Core;
using UnityEngine;

namespace Code.Infrastructure
{
    public class ClothConstraintService : IClothConstraintService
    {
        private readonly float _minConstraint;
        private readonly float _maxConstraint;

        public ClothConstraintService(float minConstraint, float maxConstraint)
        {
            _minConstraint = minConstraint;
            _maxConstraint = maxConstraint;
        }

        public void ApplyConstraints(Cloth firstCloth, Cloth secondCloth, BrokenLine brokenLine)
        {
            float maxDistance = 0;

            float[] firstDistances = GetDistances(firstCloth, brokenLine, ref maxDistance);
            float[] secondDistances = GetDistances(secondCloth, brokenLine, ref maxDistance);
            
            SetConstraints(firstCloth, firstDistances, maxDistance);
            SetConstraints(secondCloth, secondDistances, maxDistance);

        }

        private void SetConstraints(Cloth cloth, float[] distances, float maxDistance)
        {
            ClothSkinningCoefficient[] coefficients = cloth.coefficients;
            for(int i = 0; i < coefficients.Length; i++)
                coefficients[i].maxDistance = Mathf.Lerp(_maxConstraint, _minConstraint, distances[i] / maxDistance);

            cloth.coefficients = coefficients;

        }

        private float[] GetDistances(Cloth cloth, BrokenLine brokenLine, ref float maxDistance)
        {
            float[] distances = new float[cloth.coefficients.Length];
            
            for(int i = 0; i < cloth.vertices.Length; i++)
            {
                float distance = 0;
                foreach (Vector3 point in brokenLine.Points)
                    distance += Vector3.Distance(cloth.vertices[i], point);

                distances[i] = distance;
                if(distance > maxDistance)
                    maxDistance = distance;
            }

            return distances;
        }
    }
}