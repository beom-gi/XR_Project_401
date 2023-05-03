using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float characterSpeed = 0;
    public GameObject characterRender;
    void Start()
    {
        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        Color c = characterRender.GetComponent<Renderer>().material.color;              //Material 접근

        for(float fadeoffset = 1.0f; fadeoffset >= 0; fadeoffset -= 0.1f)
        {
            c.b = fadeoffset;                                   //Blue 값 감소
            c.g = fadeoffset;                                   //Green 값 감소
            characterRender.GetComponent<Renderer>().material.color = c;
            yield return new WaitForSeconds(1.0f);                  //For 구문을 1초마다 돌아가게 함
        }


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            characterSpeed = 5.0f;
        }

        transform.Translate(0, 0, characterSpeed * Time.deltaTime);

        characterSpeed *= 0.99f;

    }
}
