using UnityEngine;
using Zenject;

namespace StartMenu
{
    public class LoadStartMenu : MonoBehaviour
    {
        private SceneLoader _sceneLoader;

        [Inject]
        public void Init(SceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        public void Load()
        {
            _sceneLoader.LoadScene(SceneNames.StartMenuScene);
        }
    }
}