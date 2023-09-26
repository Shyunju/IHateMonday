using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private float healthChangeDelay = .5f;

    private PlayerHandler _playerHandler;
    private float _timeSinceLastChange = float.MaxValue;    

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;
    public event Action OnInvincibilityEnd;

    public int CurrentHealth { get; private set; }  
    public int MaxHealth => _playerHandler.GetCurrentMaxHp();

    private void Awake()
    {
        _playerHandler = GetComponent<PlayerHandler>();
    }
    void Start()
    {
        CurrentHealth = _playerHandler.GetCurrentHp();
    }

    void Update()
    {
        if (_timeSinceLastChange < healthChangeDelay)   
        {
            _timeSinceLastChange += Time.deltaTime;     
            if (_timeSinceLastChange >= healthChangeDelay)      
            {
                OnInvincibilityEnd?.Invoke();      
            }
        }
    }

    public bool ChangeHealth(int change)
    {
        if (change == 0 || _timeSinceLastChange < healthChangeDelay)
        {
            return false;
        }

        _timeSinceLastChange = 0f;
        CurrentHealth += change;
        CurrentHealth = CurrentHealth > MaxHealth ? MaxHealth : CurrentHealth;
        CurrentHealth = CurrentHealth < 0 ? 0 : CurrentHealth;

        if (change > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
        }

        if (CurrentHealth <= 0f)
        {
            CallDeath();
        }

        return true;
    }

    private void CallDeath()
    {
        OnDeath?.Invoke();
    }
}