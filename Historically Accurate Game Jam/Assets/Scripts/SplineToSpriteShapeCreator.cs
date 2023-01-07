using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.U2D;
using Spline = UnityEngine.Splines.Spline;

namespace DefaultNamespace
{
    public class SplineToSpriteShapeCreator : MonoBehaviour
    {
        public List<SplineContainer> SplineContainers;
        public SpriteShapeController ShapeControllerPrefab;
        public Vector3 LocalPositionShiftFromRailParent = Vector3.zero;
        public float ShapeBodyBaseHeight = -1;


        private void Awake()
        {
            foreach (SplineContainer splineContainer in SplineContainers)
                SpawnShape(splineContainer);
        }

        private void SpawnShape(SplineContainer splineContainer)
        {
            SpriteShapeController ShapeController = Instantiate(ShapeControllerPrefab, splineContainer.transform);
            ShapeController.transform.localPosition = LocalPositionShiftFromRailParent;

            UnityEngine.U2D.Spline shapeSpline = ShapeController.spline;
            Spline railSpline = splineContainer.Spline;

            for (int i = 0; i < railSpline.Count; i++)
            {
                BezierKnot railKnot = railSpline[i];

                if (shapeSpline.GetPointCount() <= i)
                    shapeSpline.InsertPointAt(i, railKnot.Position);
                else
                    shapeSpline.SetPosition(i, railKnot.Position);

                shapeSpline.SetTangentMode(i, ShapeTangentMode.Continuous);
                shapeSpline.SetLeftTangent(i, railKnot.TangentOut);
                shapeSpline.SetRightTangent(i, railKnot.TangentIn);
            }

            var firstPointWorldPosition = ShapeController.transform.TransformPoint(railSpline.First().Position);
            var lastPointWorldPosition = ShapeController.transform.TransformPoint(railSpline.Last().Position);

            firstPointWorldPosition.y = ShapeBodyBaseHeight;
            lastPointWorldPosition.y = ShapeBodyBaseHeight;

            shapeSpline.InsertPointAt(railSpline.Count,
                ShapeController.transform.InverseTransformPoint(firstPointWorldPosition));
            shapeSpline.InsertPointAt(railSpline.Count,
                ShapeController.transform.InverseTransformPoint(lastPointWorldPosition));

            ShapeController.splineDetail = 128;
        }
    }
}