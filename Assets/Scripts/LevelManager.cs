using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public int CurrentLevel { get; private set; } = 1; // Starts at level 1

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Optional: Keep the level manager persistent across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void NextLevel()
    {
        CurrentLevel++;
        // Load next level scene or regenerate maze here
    }

    // If you need to reset the level counter when the game restarts
    public void ResetLevels()
    {
        CurrentLevel = 1;
    }
}
