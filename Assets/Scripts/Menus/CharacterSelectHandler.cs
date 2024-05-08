using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectHandler : MonoBehaviour
{
    [SerializeField] private ScreenTransition screenTransition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void spiderSelect(){
        SceneManager.LoadScene("Forest Start");
    }
}
