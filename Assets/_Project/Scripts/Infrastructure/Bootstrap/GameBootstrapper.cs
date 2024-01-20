using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Infrastructure.Bootstrap
{
    public class GameBootstrapper : MonoBehaviour
    {
        private void Start()
        {
            DontDestroyOnLoad(this);
            SceneManager.LoadScene("GameplayTest"); // TODO: Load from a scene manager
        }
    }
}