using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Image))]
public class Point : MonoBehaviour
{
    [SerializeField] private Color _activePointColor;
    [SerializeField] private float _colorChangeSpeed;

    private Animator _animator;
    private Image _image;
    private Color _currentColor;
    private bool _isActive;

    public bool IsActive => _isActive;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _image = GetComponent<Image>();
        _currentColor = _image.color;
        _isActive = false;
    }

    public void Activate()
    {
        _animator.SetTrigger(AnimatorPoint.Trigger.Activate);
        StartCoroutine(ChangeColor());
        _isActive = true;
    }

    private IEnumerator ChangeColor()
    {
        while (_image.color != _activePointColor)
        {
            _currentColor.b -= _colorChangeSpeed * Time.deltaTime;
            _currentColor.r -= _colorChangeSpeed * Time.deltaTime;
            _image.color = _currentColor;

            yield return null;
        }
    }
}

public static class AnimatorPoint
{
    public static class Trigger
    {
        public const string Activate = nameof(Activate);
    }
}
