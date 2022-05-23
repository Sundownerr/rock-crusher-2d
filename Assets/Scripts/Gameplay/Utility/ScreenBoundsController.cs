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

        public void Add(IFactory<GameObject> factory)
        {
            factory.Created += FactoryOnObjectCreated;
            factoriesGameObject.Add(factory);
        }

        public void Add(IFactory<Transform> factory)
        {
            factory.Created += FactoryOnObjectCreated;
            factoriesTransform.Add(factory);
        }

        private void FactoryOnObjectCreated(Transform obj)
        {
            targetTransforms.Add(obj);
        }

        public void Destroy()
        {
            foreach (var factory in factoriesGameObject)
                factory.Created -= FactoryOnObjectCreated;

            foreach (var factory in factoriesTransform)
                factory.Created -= FactoryOnObjectCreated;
        }

        private void FactoryOnObjectCreated(GameObject obj)
        {
            Debug.Log("FactoryOnObjectCreated");
            targetTransforms.Add(obj.transform);
        }
    }
}