using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;
using MessagePack;
using MessagePack.Resolvers;
using MessagePack.Unity;
using MessagePack.Formatters;

public class GameManager : MonoBehaviour
{
    public bool glassesOn;
    public bool bootsOn;

    public GameObject glasses;
    public GameObject boots;

    private SaveLoadManager saveLoadManager;
    public PlayerData playerData;

    // Start is called before the first frame update

    private void Start()
    {
        playerData = new PlayerData();
        saveLoadManager = FindAnyObjectByType<SaveLoadManager>();
    }

    public void changeGlasses()
    {
        if (glassesOn == true)
        {
            glasses.SetActive(false);
            glassesOn = false;
        } else
        {
            glasses.SetActive(true);
            glassesOn = true;
        }
    }

    public void changeBoots()
    {
        if (bootsOn == true)
        {
            boots.SetActive(false);
            bootsOn = false;
        }
        else
        {
            boots.SetActive(true);
            bootsOn = true;
        }
    }

    public void changeGlassesColor()
    {
        glasses.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    public void changeBootsColor()
    {
        boots.GetComponent<Renderer>().material.color = new Color(Random.value, Random.value, Random.value);
    }

    public void changeSceneToBlackHole()
    {
        SceneManager.LoadScene(0);
    }

    public void SaveGame()
    {
        playerData.bootsColor = boots.GetComponent<Renderer>().material.color;
        playerData.glassesColor = glasses.GetComponent<Renderer>().material.color;

        saveLoadManager.SavePlayerData(playerData);
        Debug.Log("Data Saved");
    }

    public void LoadGame()
    {
        playerData = saveLoadManager.LoadPlayerData();
        
        if (playerData != null)
        {
            glasses.GetComponent<Renderer>().material.color = playerData.glassesColor;
            boots.GetComponent<Renderer>().material.color = playerData.bootsColor;
            Debug.Log(playerData.bootsColor + playerData.glassesColor);
            Debug.Log("Data Loaded");
        } else
        {
            playerData = new PlayerData();
            playerData.bootsColor = new Color(0f, 0f, 0f, 0f);
            playerData.glassesColor = new Color(0f, 0f, 0f, 0f);
        }
        
    }
}
