using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneReloader : MonoBehaviour
{
	[SerializeField] private LayerMask _layerMask;

	public static void ReloadScene()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;

		SceneManager.LoadScene(currentSceneIndex);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if ((_layerMask.value & 1 << other.gameObject.layer) != 0)
			ReloadScene();
	}
}