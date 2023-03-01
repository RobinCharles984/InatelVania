using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_PlayerData : MonoBehaviour
{
    public Data data;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        data = new Data();
    }

    // Update is called once per frame
    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();

        string sceneName = scene.name;
        /*float playerX = player.transform.position.x;
        float playerY = player.transform.position.y;
        float playerZ = player.transform.position.z;*/
        Vector3 position = player.transform.position;

        data.currentScene = sceneName;
        data.playerPosition = position;
        /*data.playerX = playerX;
        data.playerY = playerY;
        data.playerZ = playerZ;*/
    }

    private void OnApplicationQuit()
    {
        data.Save();
    }
}
