using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class gameplayScript : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] TextMeshProUGUI[] _text;
    [SerializeField] float _timeToHide = 3.0f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(gameStart());
    }

    // Update is called once per frame
    void Update()
    {

    }

    void clearText()
    {
        for (int i = 0; i < _text.Length; i++)
        {
            _text[i].text = string.Empty;
        }
    }

    IEnumerator gameStart()
    {
        yield return new WaitForSeconds(_timeToHide);
        clearText();
    }
}
