using System.Collections.Generic;
using UnityEngine;

namespace Code.Core
{
    public class BrokenLine
    {
        private readonly List<Vector3> _points = new List<Vector3>();
        
        public IEnumerable<Vector3> Points => _points;

        public void AddPoint(Vector3 point) =>
            _points.Add(point);
    }
}