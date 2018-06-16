using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GwentCard
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] string scene;

        public void OnClick()
        {
            SceneManager.LoadScene(scene);
        }
    }
}