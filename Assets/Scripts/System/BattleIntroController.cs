using System;
using Cinemachine;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

/// <summary>
/// バトルの開始演出を管理します
/// </summary>
public class BattleIntroController : MonoBehaviour
{
    [Header("カメラ")]
    [SerializeField, Tooltip("画面に近いカメラ")] private CinemachineVirtualCamera _closeUpCamera;
    [SerializeField, Tooltip("バトル全体を映す基本のカメラ")] private CinemachineVirtualCamera _battleCamera;
    
    [Header("UI")]
    [SerializeField] private Text _bossDialogueText;       // ボスのセリフを表示するUI
    [SerializeField] private RectTransform _battleStartText; // 「戦闘開始」テキストのスライド用

    [Header("タイミング")]
    [SerializeField] private float cameraZoomDuration = 2f; // カメラズーム時間
    [SerializeField] private float textSlideDuration = 1f; // テキストスライド時間

    [Header("表示する文字")]
    [SerializeField] private string bossDialogue = "我が名はデスドラゴン！貴様らを滅ぼす！";
    

    /// <summary>
    /// 演出を開始する
    /// </summary>
    public void StartBattleIntro(Action onComplete)
    {
        //CloseUpCameraを使用
        _closeUpCamera.Priority = 10;
        _battleCamera.Priority = 5;

        // ボスのセリフを表示
        ShowBossDialogue(() =>
        {
            // セリフが終わったらカメラを引く
            TransitionToBattleCamera(() =>
            {
                // 「戦闘開始」テキストをスライドイン
                ShowStartBattleText(() =>
                {
                    // 演出終了後、コマンド選択UIを表示
                    Debug.Log("演出終了：バトル開始！");
                    onComplete?.Invoke(); 
                });
            });
        });
    }

    /// <summary>
    /// ボスのセリフを表示する
    /// </summary>
    private void ShowBossDialogue(Action onComplete)
    {
        _bossDialogueText.text = bossDialogue;
        _bossDialogueText.gameObject.SetActive(true);

        // 一定時間セリフを表示した後に非表示
        DOVirtual.DelayedCall(3f, () =>
        {
            _bossDialogueText.gameObject.SetActive(false);
            onComplete?.Invoke();
        });
    }

    /// <summary>
    /// カメラの優先順位を切り替える
    /// </summary>
    /// <param name="onComplete"></param>
    private void TransitionToBattleCamera(Action onComplete)
    {
        _closeUpCamera.Priority = 5;
        _battleCamera.Priority = 10;

        // カメラ遷移が完了したらコールバックを呼ぶ
        DOVirtual.DelayedCall(cameraZoomDuration, () =>
        {
            onComplete?.Invoke();
        });
    }
    
    /// <summary>
    /// 「戦闘開始」のテキストを表示する
    /// </summary>
    private void ShowStartBattleText(Action onComplete)
    {
        _battleStartText.anchoredPosition = new Vector2(800, 0); // 初期位置（画面外）
        _battleStartText.DOAnchorPos(Vector2.zero, textSlideDuration).SetEase(Ease.OutBounce).OnComplete(() =>
        {
            // 一定時間後にテキストを非表示
            DOVirtual.DelayedCall(2f, () =>
            {
                _battleStartText.gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        });
    }
}
