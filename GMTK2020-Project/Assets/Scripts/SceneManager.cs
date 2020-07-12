using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TMG.GMTK2020
{
    public class SceneManager : MonoBehaviour
    {
		//private static SceneManager _instance;
        public static SceneManager instance;

		SceneManager sceneManager;

		private void Awake()
		{
			if (instance != null)
			{
				Destroy(gameObject);
			}
			else
			{
				instance = this;
				DontDestroyOnLoad(this);
			}


		}

		public void ReloadScene()
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene("BattleScene");
		}

		public void LoadNewScene(string sceneName)
		{
			UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
		}
	}
}