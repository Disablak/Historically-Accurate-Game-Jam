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


        [ContextMenu("SpawnShape")]
        private void SpawnShape()
        {
            SpawnShape(SplineContainers.First());
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

                if (i != railSpline.Count - 1 && i != 0)
                    shapeSpline.SetTangentMode(i, ShapeTangentMode.Continuous);
                else
                    shapeSpline.SetTangentMode(i, ShapeTangentMode.Linear);

                shapeSpline.SetLeftTangent(i, railKnot.TangentOut.WithoutYParameter());
                shapeSpline.SetRightTangent(i, railKnot.TangentIn.WithoutYParameter());
            }
            //
            // var firstPointWorldPosition = ShapeController.transform.TransformPoint(railSpline.First().Position);
            // var lastPointWorldPosition = ShapeController.transform.TransformPoint(railSpline.Last().Position);
            //
            // firstPointWorldPosition.y = ShapeBodyBaseHeight;
            // lastPointWorldPosition.y = ShapeBodyBaseHeight;
            //
            // shapeSpline.InsertPointAt(railSpline.Count,
            //     ShapeController.transform.InverseTransformPoint(firstPointWorldPosition));
            // shapeSpline.InsertPointAt(railSpline.Count,
            //     ShapeController.transform.InverseTransformPoint(lastPointWorldPosition));

            ShapeController.splineDetail = 128;
        }
    }
}