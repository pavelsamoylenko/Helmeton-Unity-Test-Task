using UnityEngine.SceneManagement;
using Zenject;

namespace AR
{
	public class ARContentLoader : IInitializable
	{
		private readonly ARContentSceneID _sceneID;

		public ARContentLoader(ARContentSceneID sceneID)
		{
			_sceneID = sceneID;
		}

		public void Initialize()
		{
			SceneManager.LoadSceneAsync(_sceneID.SceneName, LoadSceneMode.Additive);
		}
	}
}
