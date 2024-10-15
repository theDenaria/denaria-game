using _Project.StrangeIOCUtility;
using _Project.StrangeIOCUtility.Scripts.Views;
using strange.extensions.signal.impl;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.GameSceneManager.Scripts.Views
{
    public class GameSceneView : ViewZeitnot
    {
        public string SceneName { get; set; }

        internal Signal onSceneLoaded = new Signal();

        [SerializeField] public GameObject OwnPlayerPrefab;
        [SerializeField] public GameObject EnemyPlayerPrefab;

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // called second
        void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            SceneName = scene.name;
            onSceneLoaded.Dispatch();
        }

        void OnDisable()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }
    }
}