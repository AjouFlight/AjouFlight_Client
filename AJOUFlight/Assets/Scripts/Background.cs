using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private Sprite[] backgroundSprites;

    private int backgroundSpriteNum = 4;


    void Start()
    {
        backgroundSpriteNum = 4;
        InitSetSpriteBackground();
        StartCoroutine(ScrollBackground());
    }
    
    private void InitSetSpriteBackground()
    {
        int stage = PlayerInformation.currentStage;

        for(int i=0; i<backgroundSpriteNum; i++)
        {
            SpriteRenderer child = transform.GetChild(i).GetComponent<SpriteRenderer>();
            child.sprite = backgroundSprites[stage - 1];
        }
    }


    IEnumerator ScrollBackground()
    {
        while (true)
        {
            if (transform.position.y < -80.0f)
                transform.position = Vector3.zero;

            transform.position += new Vector3(0, -0.05f);
            yield return null;
        }
    }

}