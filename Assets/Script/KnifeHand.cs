using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KnifeHand : MonoBehaviour
{
    [SerializeField] private KnifeTip _knifeTip;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _returnRotateSpeed;
    [SerializeField] private float _returnDelay;
    [SerializeField] private float _topTipPosition;
    [SerializeField] private float _downTipPosition;
    [SerializeField] private Quaternion _downRotation;
    [SerializeField] private Quaternion _upRotation;

    public event UnityAction SliceCut;
    public event UnityAction CutIsOver;

    public void StartToCut()
    {
        StartCoroutine(Cut());
    }

    private IEnumerator Cut()
    {
        while (_knifeTip.transform.position.y >= _downTipPosition)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _downRotation, _rotateSpeed * Time.deltaTime);

            yield return null;
        }

        SliceCut?.Invoke();

        yield return new WaitForSeconds(_returnDelay);
        StartCoroutine(SetStartRotation());
    }

    private IEnumerator SetStartRotation()
    {
        while (_knifeTip.transform.position.y <= _topTipPosition)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _upRotation, _returnRotateSpeed * Time.deltaTime);

            yield return null;
        }

        CutIsOver?.Invoke();
    }
}
