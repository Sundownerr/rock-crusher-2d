using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class ScreenBoundsController : IUpdate
    {
        private readonly Camera camera;
        private readonly List<Transform> targetTransforms = new List<Transform>();

        public ScreenBoundsController()
        {
            camera = Camera.main;
        }

        public void Update()
        {
            for (var index = 0; index < targetTransforms.Count; index++)
            {
                var targetTransform = targetTransforms[index];

                if (targetTransform == null)
                {
                    targetTransforms.Remove(targetTransform);
                    continue;
                }

                var positionOnScreen = camera.WorldToScreenPoint(targetTransform.position);
                var outOfBoundsX = positionOnScreen.x >= Screen.width || positionOnScreen.x <= 0;
                var outOfBoundsY = positionOnScreen.y >= Screen.height || positionOnScreen.y <= 0;

                if (outOfBoundsX)
                {
                    var mirroredX = Screen.width - positionOnScreen.x;
                    var mirrorPosition = new Vector3(mirroredX, positionOnScreen.y, 10);
                    targetTransform.position = camera.ScreenToWorldPoint(mirrorPosition);
                }

                if (outOfBoundsY)
                {
                    var mirroredY = Screen.height - positionOnScreen.y;

                    var mirrorPosition = new Vector3(positionOnScreen.x, mirroredY, 10);
                    targetTransform.position = camera.ScreenToWorldPoint(mirrorPosition);
                }
            }
        }

        public void Add(Transform targetTransform)
        {
            targetTransforms.Add(targetTransform);
        }
    }
}