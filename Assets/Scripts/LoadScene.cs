using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour {
    [SerializeField] string scene;

	public void OnClick()
    {
        SceneManager.LoadScene(scene);
    }
}
