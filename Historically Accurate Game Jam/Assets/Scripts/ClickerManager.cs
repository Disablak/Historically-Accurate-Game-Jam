using System;
using System.Collections;
using DefaultNamespace;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


public class ClickerManager : MonoBehaviour
  {
    private int               _seconds_left;
    private ClickableResource _clickable_resource;
    private Bounds            _bonus_spawn_bounds;
    private bool              _is_playable;

    private event Action timerEnding;
    private event Action timeEnded;

    [SerializeField] private ClickerUI clickerUI;
    [SerializeField] private Object    clickableResourcePrefab;
    [SerializeField] private Object    clickableBonusPrefab;
    [SerializeField] private int       secondsToMine;
    [SerializeField] private float     timeRunningOutSeconds;


    private int _resources_mined;
    private int _diamonds_mined;

    private int secondsLeft
    {
      get => _seconds_left;
      set
      {
        clickerUI.setTimer(value);
        _seconds_left = value;

        if (value <= 0)
          timeEnded?.Invoke();
        else
        if (value <= timeRunningOutSeconds)
          timerEnding?.Invoke();
      }
    }


    private void Start()
    {
      _clickable_resource = Instantiate( clickableResourcePrefab ).GetComponent<ClickableResource>();
      _clickable_resource.onClicked += onClicked;

      secondsLeft = secondsToMine;

      _bonus_spawn_bounds = _clickable_resource.collider2D.bounds;
      _bonus_spawn_bounds.Expand(_bonus_spawn_bounds.size * -0.3f);

      timerEnding += clickerUI.timerEnding;
      timeEnded += clickerUI.timerEnded;
      timeEnded += endClicker;

      startAll();

      _is_playable = true;
    }

    private void onClicked(int resource_count)
    {
      if(!_is_playable)
        return;

      _clickable_resource.tween();
      if(Random.Range(0, 11) < 8)
        setResourceMined(resource_count);
      else
        setDiamondMined();
    }

    private void setResourceMined(int resource_count)
    {
      _resources_mined += resource_count;
      clickerUI.resourceMined(resource_count, _resources_mined);
    }

    private void setDiamondMined()
    {
      clickerUI.diamondMined(1, ++_diamonds_mined);
    }

    private IEnumerator spawnBonusCoroutine()
    {
      while (true)
      {
        float wait_seconds = Random.Range(3.0f, 10.0f);
        Debug.Log(wait_seconds);
        yield return new WaitForSeconds(wait_seconds);
        spawnBonus();
      }
    }

    private IEnumerator timerCoroutine()
    {
      while (secondsLeft > 0)
      {
        yield return new WaitForSeconds(1.0f);
        --secondsLeft;
      }
    }

    private void OnDisable()
    {
      stopAll();
    }

    private void startAll()
    {
      StartCoroutine(spawnBonusCoroutine());
      StartCoroutine(timerCoroutine());
    }

    private void stopAll()
    {
      StopAllCoroutines();
    }

    private void spawnBonus()
    {
      Vector3 spawn_position = new Vector3(Random.Range(_bonus_spawn_bounds.min.x, _bonus_spawn_bounds.max.x), Random.Range(_bonus_spawn_bounds.min.y, _bonus_spawn_bounds.max.y));
      Object game_object = Instantiate(clickableBonusPrefab, spawn_position, Quaternion.identity);
      game_object.GetComponent<Transform>().Translate(Vector3.back);
      ClickableResource clickable_resource = game_object.GetComponent<ClickableResource>();
      clickable_resource.onClicked += onClicked;
    }

    private void endClicker()
    {
      _is_playable = false;
      stopAll();
    }
  }