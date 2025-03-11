using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChange : MonoBehaviour
{
     [SerializeField] private string sceneName; //make sure target scene is in build settings
    
    void Start()
    {
        
    }

     private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(sceneName); //reloads the current scene on death
        }
    }

}
