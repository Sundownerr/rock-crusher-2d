using System;

namespace Game.Scenes.Interface
{
    public interface ISceneLoader
    {
        event Action GameplaySceneLoaded;
        event Action GameplayUISceneLoaded;
        event Action MenuUISceneLoaded;
        event Action GameoverUISceneLoaded;

        event Action GameplaySceneUnloaded;
        event Action GameplayUISceneUnloaded;
        event Action MenuUISceneUnoaded;
        event Action GameoverUISceneUnoaded;

        public void Destroy();
    }
}