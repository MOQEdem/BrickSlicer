using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private Knife _knife;
    [SerializeField] private Brick _brick;
    [SerializeField] private KnifeHand _knifeHand;
    [SerializeField] private UIController _uiController;
    [SerializeField] private EndGameScreen _screen;
    [SerializeField] private GameUI _gameUI;

    private void OnEnable()
    {
        _knife.CutBrick += OnBrickCut;
        _knifeHand.CutIsOver += OnCutIsOver;
    }

    private void OnDisable()
    {
        _knife.CutBrick -= OnBrickCut;
        _knifeHand.CutIsOver -= OnCutIsOver;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _brick.SetMovingStatus(false);
            _knifeHand.StartToCut();
        }
    }

    private void OnBrickCut()
    {
        _brick.Cut();
    }

    private void OnCutIsOver()
    {

        if (_uiController.IsAllPointsActive())
        {
            _gameUI.gameObject.SetActive(false);
            _screen.gameObject.SetActive(true);
        }
        else
        {
            _brick.SetMovingStatus(true);
        }
    }
}
