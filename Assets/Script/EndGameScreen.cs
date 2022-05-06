using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class EndGameScreen : MonoBehaviour
{
    [SerializeField] private Image _back;
    [SerializeField] private Animator _text;
    [SerializeField] private Animator _button;
    [SerializeField] private float _shadingStep;
    [SerializeField] private float _maxShading;

    private void Awake()
    {
        _text.gameObject.SetActive(false);
        _back.gameObject.SetActive(false);
        _button.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        StartCoroutine(ShowScreen());
    }

    private IEnumerator ShowScreen()
    {
        _text.gameObject.SetActive(true);
        _text.SetTrigger(AnimatorText.Trigger.Win);

        _button.gameObject.SetActive(true);
        _button.SetTrigger(AnimatorButton.Trigger.Win);

        _back.gameObject.SetActive(true);
        while (_back.color.a < _maxShading)
        {
            Color color = _back.color;
            color.a += _shadingStep * Time.deltaTime;
            _back.color = color;

            yield return null;
        }
    }

    public static class AnimatorText
    {
        public static class Trigger
        {
            public const string Win = nameof(Win);
        }
    }

    public static class AnimatorButton
    {
        public static class Trigger
        {
            public const string Win = nameof(Win);
        }
    }
}