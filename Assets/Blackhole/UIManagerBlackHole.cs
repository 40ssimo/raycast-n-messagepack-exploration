using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerBlackHole : MonoBehaviour
{
    // Start is called before the first frame update
    public void changeSceneToHideAndSeek()
    {
        SceneManager.LoadScene(1);
    }
}
