using System;
using System.Collections.Generic;
using UnityEngine;

public class BattleView : MonoBehaviour
{
    [SerializeField] private CharacterView[] _playerViews;
    [SerializeField] private CharacterView[] _enemyViews;
    [SerializeField] private BattleIntroController _battleIntroController;

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

    /// <summary>
    /// 開始演出を再生する
    /// </summary>
    public void ShowBattleStartAnimation(Action onComplete)
    {
        _battleIntroController.StartBattleIntro(()=>
        {
            Debug.Log("アニメーション完了");
            onComplete?.Invoke();
        });
    }
}
