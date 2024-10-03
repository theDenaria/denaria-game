// using UnityEngine;
// using UnityEngine.SceneManagement;
// using System.Collections;

// using TMPro;
// using UnityEngine.UI;

// public class SystemManager : MonoBehaviour
// {
//     public static SystemManager Instance { get; private set; }
//     [SerializeField] private ClientBehaviour clientBehaviour;
//     public TMP_InputField playerIdInput;
//     public string ownPlayerId;

//     private GameManager gameManager = null;

//     void Awake()
//     {
//         if (Instance == null)
//         {
//             Instance = this;
//             DontDestroyOnLoad(gameObject);
//         }
//         else
//         {
//             Destroy(gameObject);
//         }
//     }

//     public void InitGameManager() // Signal / Command in NetworkManagementModel
//     {
//         gameManager = GameManager.Instance;
//         clientBehaviour.InitGameManager();
//     }

//     public void ConnectButtonClicked() // View/Mediator
//     {
//         ownPlayerId = playerIdInput.text;
//         LoadGameSceneAsync();
//     }

//     public void LoadGameSceneAsync()
//     {
//         StartCoroutine(LoadGameSceneAsyncCoroutine());
//     }

//     private IEnumerator LoadGameSceneAsyncCoroutine() // TODO:ONAT View/Mediator
//     {
//         AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("GameScene");
//         while (!asyncLoad.isDone)
//         {
//             // Update loading screen progress
//             float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // Normalized progress in percent
//             Debug.Log("Loading progress: " + (progress * 100) + "%");
//             yield return null;
//         }
//     }

//     public void Disconnect() // Command
//     {
//         clientBehaviour.Disconnect();
//         LoadMainMenuSceneAsync();
//         ownPlayerId = default;
//     }

//     private void LoadMainMenuSceneAsync() // TODO:ONAT
//     {
//         StartCoroutine(LoadMainMenuSceneAsyncCoroutine());
//     }

//     private IEnumerator LoadMainMenuSceneAsyncCoroutine() // TODO:ONATView/Mediator
//     {
//         AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu");

//         while (!asyncLoad.isDone)
//         {
//             // Update loading screen progress
//             float progress = Mathf.Clamp01(asyncLoad.progress / 0.9f); // Normalized progress in percent
//             Debug.Log("Loading progress: " + (progress * 100) + "%");
//             yield return null;
//         }
//         CleanupGameState();
//         SetupUIBindings();
//     }

//     private void SetupUIBindings() // View/Mediator
//     {
//         // Assume ConnectButton is the tag for your button, adjust accordingly
//         Button connectButton = GameObject.FindGameObjectWithTag("ConnectButton").GetComponent<Button>();
//         if (connectButton != null)
//         {
//             connectButton.onClick.RemoveAllListeners();  // Remove existing listeners to prevent duplicates
//             connectButton.onClick.AddListener(ConnectButtonClicked);  // Re-add listener
//         }
//         playerIdInput = FindObjectOfType<TMP_InputField>();
//     }

//     private void CleanupGameState() // Command
//     {
//         Destroy(gameManager.gameObject);
//     }
// }