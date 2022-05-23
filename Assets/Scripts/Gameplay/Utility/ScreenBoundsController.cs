using System.Collections.Generic;
using UnityEngine;

namespace Game.Gameplay.Utility
{
    public class ScreenBoundsController : IUpdate
    {
        private readonly Camera camera;
        private readonly List<IFactory<GameObject>> factoriesGameObject = new List<IFactory<GameObject>>();
        private readonly List<IFactory<Transform>> factoriesTransform = new List<IFactory<Transform>>();
        private readonly List<Transform> targetTransforms = new List<Transform>();

        public ScreenBoundsController(Camera camera)
        {
            this.camera = camera;
        }

        public void Update()
        {
            for (var i = 0; i < targetTransforms.Count; i++)
            {
                var targetTransform = targetTransforms[i];

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

        private void OnObjectCreated(Transform obj) => Add(obj);
        private void OnObjectCreated(GameObject obj) => Add(obj.transform);

        public void Add(IFactory<GameObject> factory)
        {
            factory.Created += OnObjectCreated;
            factoriesGameObject.Add(factory);
        }

        public void Add(IFactory<Transform> factory)
        {
            factory.Created += OnObjectCreated;
            factoriesTransform.Add(factory);
        }

        public void Destroy()
        {
            foreach (var factory in factoriesGameObject)
                factory.Created -= OnObjectCreated;

            foreach (var factory in factoriesTransform)
                factory.Created -= OnObjectCreated;
        }
    }
}