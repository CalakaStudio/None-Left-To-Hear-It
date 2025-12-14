using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeDemoScreen : MonoBehaviour
{
    [SerializeField] SceneField sceneToLoad;
    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

}
