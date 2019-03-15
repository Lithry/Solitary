using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIControls : MonoBehaviour {

    public void ResetBTM() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
