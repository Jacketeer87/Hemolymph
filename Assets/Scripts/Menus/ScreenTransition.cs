using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ScreenTransition : MonoBehaviour
{
    [SerializeField] private Image slideImage;
    [SerializeField] private float slideTime = 1;
    [SerializeField] private float slideSpeed = 500f;
    [SerializeField] private float slideDistance = 1000f;
    [SerializeField] private float transitionDelay = 1;

    private RectTransform imageRectTransform;
    private Vector3 originalPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ThatsAllFolks(string newScene = "")
    {
        imageRectTransform = slideImage.GetComponent<RectTransform>();
        originalPosition = imageRectTransform.localPosition; 
        StartCoroutine(SlideRoutine());
        IEnumerator SlideRoutine(){

            Vector3 targetPosition = Vector3.zero;
            float timer = 0;
            while(timer < transitionDelay){
                yield return null;
                timer+=Time.deltaTime;
                imageRectTransform.localPosition = Vector3.Lerp(originalPosition, targetPosition, timer / transitionDelay);
            }

            imageRectTransform.localPosition = targetPosition;

            SceneManager.LoadScene(newScene);
            
        }

    }
}
