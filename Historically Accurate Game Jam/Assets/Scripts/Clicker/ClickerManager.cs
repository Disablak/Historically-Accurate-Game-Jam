using System;
using System.Collections;
using System.Linq;
using Clicker;
using Core;
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

    [Header("References")]
    [SerializeField] private Object    clickableBonusPrefab;
    [SerializeField] private Object    clickableResourcePrefab;
    [SerializeField] private Transform resourcePosition;
    [SerializeField] private ClickerUI clickerUI;

    [Header("Settings")]
    [SerializeField] private int   secondsToMine;
    [SerializeField] private float timeRunningOutSeconds;


    private int _coal_mined;
    private int _gold_mined;
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
      _clickable_resource = Instantiate( clickableResourcePrefab, resourcePosition ).GetComponent<ClickableResource>();
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

    private void onClicked(ClickableResourceScriptableObject settings)
    {
      if(!_is_playable)
        return;

      _clickable_resource.tween();
      Tuple<ResourceType, int> result = settings.getRandomResourceTypeWithValue();
      setResourceMined(result.Item1, result.Item2);
    }

    private void setResourceMined(ResourceType resource_type, int resource_count)
    {
      int total = 0;
      switch (resource_type)
      {
        case ResourceType.COAL:    _coal_mined += resource_count; total = _coal_mined; break;
        case ResourceType.GOLD:    _gold_mined += resource_count; total = _gold_mined; break;
        case ResourceType.DIAMOND: _diamonds_mined += resource_count; total = _diamonds_mined; break;
      }
      clickerUI.resourceMined(resource_type, resource_count, total);
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