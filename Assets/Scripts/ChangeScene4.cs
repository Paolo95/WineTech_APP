using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class ChangeScene4 : MonoBehaviour
{
    public void btn_change_scene(string scene_name)
    {
        SceneManager.LoadScene(5);
    }
}
