using UnityEngine;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using Firebase;
using Firebase.Firestore;
using Firebase.Extensions;

public class FirestoreManager : MonoBehaviour
{
    public static FirestoreManager Instance; // Singleton instance

    private FirebaseFirestore _firestore;

    private void Awake()
    {
        Debug.Log("FirestoreManager Awake");
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        Debug.Log("FirestoreManager Start");
        InitializeFirebase();
    }

    private void InitializeFirebase()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                _firestore = FirebaseFirestore.DefaultInstance;
                Debug.Log("Firebase Firestore Initialized");
                SaveDataToFirestore("test", "test", "12345", "Test Name", DateTimeOffset.UtcNow.ToUnixTimeSeconds());
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
            }
        });
    }

    public void SaveDataToFirestore(string collectionName, string documentName, string id, string name, long timestamp)
    {
        if (_firestore == null)
        {
            Debug.LogError("Firestore not initialized.");
            return;
        }

        Dictionary<string, object> data = new Dictionary<string, object>
        {
            { "id", id },
            { "name", name },
            { "timestamp", timestamp }
        };

        _firestore.Collection(collectionName).Document(documentName).SetAsync(data).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                Debug.Log("Document successfully written!");
            }
            else
            {
                Debug.LogError($"Failed to write document: {task.Exception}");
            }
        });
    }
}