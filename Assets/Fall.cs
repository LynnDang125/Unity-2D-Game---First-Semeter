using UnityEngine;
using UnityEngine.SceneManagement;
public class Fall : MonoBehaviour
{
       private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //reloads the current scene on death
        }
    }
}
