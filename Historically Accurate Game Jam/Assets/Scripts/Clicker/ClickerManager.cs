using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Carting;
using Clicker;
using Core;
using UI.Clicker;
using Unity.VisualScripting;
using UnityEngine;
using Upgrades;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;


public class ClickerManager : MonoBehaviour
{
  private int               _seconds_left;
  private ClickableResource _clickable_resource;
  private Bounds            _bonus_spawn_bounds;
  private bool              _is_playable;
  private int               _cart_filled;

  private event Action timerEnding;
  private event Action gameEnded;

  [Header("References")] [SerializeField]
  private Object clickableBonusPrefab;

  [SerializeField] private Object              clickableResourcePrefab;
  [SerializeField] private Transform           resourcePosition;
  [SerializeField] private ClickerUI           clickerUI;
  [SerializeField] private ClickerSoundManager clickerSoundManager;

  [Header("Settings")]
  [SerializeField] private int   secondsToMine;
  [SerializeField] private float timeRunningOutSeconds;
  [SerializeField] private int   cartCapacity;
  [SerializeField] private ClickableResourceScriptableObject defaultResourceValueChanceSettings;
  [SerializeField] private ClickableResourceScriptableObject bonusResourceValueChanceSettings;

  private Dictionary<ResourceType, int> resourcesMined { get; set; } = new Dictionary<ResourceType, int>();

  private Dictionary<ResourceType, int> doubleMinedChance         { get; set; } = new Dictionary<ResourceType, int>();
  private Dictionary<ResourceType, int> doubleMinedChanceForBonus { get; set; } = new Dictionary<ResourceType, int>();

  private float helperMineSeconds { get; set; } = -1;

  private event Action<ResourceType, int, int, bool> resourceMined; 
  private event Action<int>                          cartFilled;

  private int curCartFilled
  {
    get => _cart_filled;
    set
    {
      _cart_filled = value;
      cartFilled?.Invoke(_cart_filled);
    }
  }

  private int secondsLeft
  {
    get => _seconds_left;
    set
    {
      clickerUI.setTimer(value);
      _seconds_left = value;

      if (value <= 0)
        gameEnded?.Invoke();
      else if (value <= timeRunningOutSeconds)
        timerEnding?.Invoke();
    }
  }


  private void Start()
  {
    foreach (ResourceType resource_type in ResourceTypeHelper.allValues)
    {
      resourcesMined[resource_type]            = 0;
      doubleMinedChance[resource_type]         = 0;
      doubleMinedChanceForBonus[resource_type] = 0;
    }

    _clickable_resource = Instantiate(clickableResourcePrefab, resourcePosition).GetComponent<ClickableResource>();
    _clickable_resource.onClicked += onClicked;

    MUProcessor.process(this);

    secondsLeft = secondsToMine;

    clickerUI.setupUI(cartCapacity);

    _bonus_spawn_bounds = _clickable_resource.collider2D.bounds;
    _bonus_spawn_bounds.Expand(_bonus_spawn_bounds.size * -0.3f);

    timerEnding += clickerUI.timerEnding;
    timerEnding += clickerSoundManager.playTimer;
    gameEnded += clickerUI.gameEnded;
    gameEnded += endClicker;
    resourceMined += clickerUI.resourceMined;
    resourceMined += clickerSoundManager.playMinedSound;
    cartFilled += clickerUI.setCartCapacity;

    _is_playable = true;

    startAll();
  }

  private void onClicked(bool is_bonus)
  {
    if (_is_playable)
      mineRandomResource(is_bonus);
  }

  private void tryPutToCartWithEndGame(int amount, out int filled)
  {
    if (curCartFilled + amount < cartCapacity)
    {
      filled = amount;
      curCartFilled += amount;
      return;
    }

    filled = cartCapacity - curCartFilled;
    curCartFilled = cartCapacity;
    gameEnded?.Invoke();
    cartFilled?.Invoke(curCartFilled);
  }

  private void mineRandomResource(bool is_bonus = false, bool is_helper = false)
  {
    ClickableResourceScriptableObject settings = is_bonus ? bonusResourceValueChanceSettings : defaultResourceValueChanceSettings;
    int random_value = Random.Range(0, 100);
    KeyValuePair<ResourceType, ResourceValueChance> resource = settings.getResourceValueChanceWithType().LastOrDefault(x => x.Value.randomRange.minInclusive <= random_value);
    if (resource.Key == ResourceType.NONE)
      resource = settings.getResourceValueChanceWithType().First();

    random_value = Random.Range(0, 100);
    Dictionary<ResourceType, int> double_chances = is_bonus ? doubleMinedChanceForBonus : doubleMinedChance;
    if (random_value < double_chances[resource.Key])
      resource.Value.amount *= 2;

    tryPutToCartWithEndGame(resource.Value.amount, out int filled);
    if(!is_helper)
      _clickable_resource.tween();
    setResourceMined(resource.Key, filled, is_helper);
  }

  private void setResourceMined(ResourceType resource_type, int resource_count, bool is_helper)
  {
    resourcesMined[resource_type] += resource_count;
    resourceMined?.Invoke(resource_type, resource_count, resourcesMined[resource_type], is_helper);
  }

  private IEnumerator spawnBonusCoroutine()
  {
    while (_is_playable)
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

  private IEnumerator helperCoroutine()
  {
    while (_is_playable)
    {
      yield return new WaitForSeconds(helperMineSeconds);
      mineRandomResource(is_helper: true);
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

    if (helperMineSeconds > 0)
      StartCoroutine(helperCoroutine());
  }

  private void stopAll()
  {
    StopAllCoroutines();
  }

  private void spawnBonus()
  {
    Vector3 spawn_position = new Vector3(Random.Range(_bonus_spawn_bounds.min.x, _bonus_spawn_bounds.max.x),
        Random.Range(_bonus_spawn_bounds.min.y, _bonus_spawn_bounds.max.y));
    Object game_object = Instantiate(clickableBonusPrefab, spawn_position, Quaternion.identity);
    game_object.GetComponent<Transform>().Translate(Vector3.back);
    ClickableResource clickable_resource = game_object.GetComponent<ClickableResource>();
    clickable_resource.onClicked += onClicked;
  }

  private void endClicker()
  {
    _is_playable = false;
    ModulesCommon.ModulePlayer.resourcesMined = resourcesMined;
    ModulesCommon.ModuleCart.setResourcesRemained(resourcesMined);
    stopAll();
    StartCoroutine(loadNextSceneCoroutine());
  }

  private IEnumerator loadNextSceneCoroutine()
  {
    yield return new WaitForSeconds(3f);
    ModulesCommon.ModuleCart.endCarting();
    ModulesCommon.loadNextScene();
  }

#region Upgrades
  public void addSecondsToMine(int bonus_seconds) => secondsToMine += bonus_seconds;

  public void addCartCapacity(int bonus_capacity) => cartCapacity += bonus_capacity;

  public void addPermanentResourceBonus(ResourceType[] resource_types, int amount)
  {
    foreach (ResourceType resource_type in resource_types)
    {
      defaultResourceValueChanceSettings.addResourceMiningAmount(resource_type, amount);
      bonusResourceValueChanceSettings.addResourceMiningAmount(resource_type, amount);
    }
  }

  public void addBonusChance(ResourceType[] resource_types, int amount, bool modify_bonus)
  {
    foreach (ResourceType resource_type in resource_types)
    {
      defaultResourceValueChanceSettings.modifyResourceMiningChance(resource_type, amount);
      if (modify_bonus)
        bonusResourceValueChanceSettings.modifyResourceMiningChance(resource_type, amount);
    }
  }

  public void addDoubleChanceBonus(ResourceType[] resource_types, int amount, bool modify_bonus)
  {
    foreach (ResourceType resource_type in resource_types)
    {
      doubleMinedChance[resource_type] += amount;
      if (modify_bonus)
        doubleMinedChanceForBonus[resource_type] += amount;
    }
  }

  public void setHelperMineSeconds(float mine_seconds) => helperMineSeconds = mine_seconds;
#endregion
}