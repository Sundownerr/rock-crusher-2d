using System;
using UnityEngine;

namespace Game.Scenes
{
    [Serializable]
    public class SceneData
    {
        [SerializeField] public string Gameplay;
        [SerializeField] public string MenuUI;
        [SerializeField] public string GameplayUI;
        [SerializeField] public string GameoverUI;
    }
}