using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemHandler : MonoBehaviour
{
    // Effect - time
    public Dictionary<ItemEffect, float> ActiveEffects { get; set; }

    public event Action<ItemEffect> OnEffectEnded;
    
    // 코루틴 관련 필드
    float updateTime = 0.1f;
    Coroutine updateCoroutine;
    WaitForSecondsRealtime waitingTime;


    void Awake()
    {
        GameManager.Instance.SetItemHandler(this);

        ActiveEffects = new Dictionary<ItemEffect, float>();
        waitingTime = new WaitForSecondsRealtime(updateTime);
        updateCoroutine = null;
    }

    void OnEnable()
    {
        if (updateCoroutine == null)
            updateCoroutine = StartCoroutine(UpdateEffectTime());
    }

    void OnDisable()
    {
        if (updateCoroutine != null)
        {
            StopCoroutine(updateCoroutine);
            updateCoroutine = null;
        }
    }


    // Update 같이 매 프레임 갱신해줄 필요가 없어서 코루틴 사용
    IEnumerator UpdateEffectTime()
    {
        while(true)
        {
            ItemEffect[] keys = ActiveEffects.Keys.ToArray();
            foreach (ItemEffect key in keys)
            {
                ActiveEffects[key] -= updateTime;
                
                if (ActiveEffects[key] <= 0f)
                {
                    Debug.Log("Effect ended");
                    OnEffectEnded?.Invoke(key);

                    ActiveEffects.Remove(key);
                }
            }

            yield return waitingTime;
        }
    }

    public bool ActivateEffect(ItemEffect effect)
    {
        if (ActiveEffects.ContainsKey(effect) && ActiveEffects[effect] > 0f)
        {
            ActiveEffects[effect] += effect.effectDuration;
            return false;
        }
        else
        {
            ActiveEffects.Add(effect, effect.effectDuration);
            return true;
        }
    }
}
