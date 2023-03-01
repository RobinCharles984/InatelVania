using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class Data
{
    [SerializeField]
    public float playerX;
    
    [SerializeField]
    public float playerY;
    
    [SerializeField]
    public float playerZ;

    [SerializeField] 
    public String currentScene;
    public Vector3 playerPosition;
    
    private string path = "Assets/Data.txt";

    public void Save()
    {
        var content = JsonUtility.ToJson(this, true);
        File.WriteAllText(path, content);
    }

    public void Load()
    {
        var content = File.ReadAllText(path);
        var p = JsonUtility.FromJson<Data>(content);

        currentScene = p.currentScene;
        SceneManager.LoadScene(currentScene);
        playerPosition = p.playerPosition;
        /*playerX = p.playerX;
        playerY = p.playerY;
        playerZ = p.playerZ;
        */

    }
}
