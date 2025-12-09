using UnityEngine;
using UnityEngine.SceneManagement;


public class ChangeDemoScreen : MonoBehaviour
{
    [SerializeField] SceneField sceneToLoad;
    bool HasClickedOnce = false;
    public void ChangeScene()
    {
        if(HasClickedOnce == false)
        {
            HasClickedOnce = true;
        }
        else
        {
            SceneManager.LoadScene(sceneToLoad);
        }
    }

}
