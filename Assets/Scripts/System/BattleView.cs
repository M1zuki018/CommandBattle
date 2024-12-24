using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleView : MonoBehaviour
{
    [SerializeField] private CharacterView[] _playerViews;
    [SerializeField] private CharacterView[] _enemyViews;

    public void UpdateAllViews(List<CharacterModel> players, List<CharacterModel> enemies)
    {
        if (players.Count > _playerViews.Length || enemies.Count > _enemyViews.Length)
        {
            Debug.LogError("View arrays do not match the number of characters!");
            return;
        }
        
        for (int i = 0; i < players.Count; i++)
        {
            if (_playerViews[i] == null)
            {
                Debug.LogError($"playerViews[{i}] is null!");
                continue;
            }
            _playerViews[i].UpdateView(players[i]);
        }

        for (int i = 0; i < enemies.Count; i++)
        {
            if (_enemyViews[i] == null)
            {
                Debug.LogError($"enemyViews[{i}] is null!");
                continue;
            }
            _enemyViews[i].UpdateView(enemies[i]);
        }
    }

    public void ShowBattleStartAnimation(Action onComplete)
    {
        Debug.Log("アニメーションスタート");

        // 指定時間後にコールバックを呼び出す
        StartCoroutine(WaitForAnimation(1f, onComplete));
    }
    
    private IEnumerator WaitForAnimation(float duration, Action onComplete)
    {
        yield return new WaitForSeconds(duration);

        // コールバックを実行
        onComplete?.Invoke();
    }
}
