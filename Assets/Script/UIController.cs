using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private List<Point> _points;
    [SerializeField] private KnifeHand _knifeHand;

    private int _currentPoint;

    private void Awake()
    {
        _currentPoint = 0;
    }

    private void OnEnable()
    {
        _knifeHand.SliceCut += OnSliceCut;
    }

    private void OnDisable()
    {
        _knifeHand.SliceCut -= OnSliceCut;
    }

    public bool IsAllPointsActive()
    {
        foreach (var point in _points)
        {
            if (!point.IsActive)
            {
                return false;
            }
        }

        return true;
    }

    private void OnSliceCut()
    {
        _points[_currentPoint].Activate();

        if (_currentPoint < _points.Count - 1)
        {
            _currentPoint++;
        }
    }
}
