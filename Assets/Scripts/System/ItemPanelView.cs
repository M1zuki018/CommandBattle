using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class ItemPanelView : MonoBehaviour
{
    [SerializeField] private GameObject _itemButtonPrefab;
    [SerializeField] private Transform _content;
    private List<Button> _itemButtons = new List<Button>(); // 作成済みのアイテムボタンリスト
    private ItemData _selectedItem;
    
    private List<ItemData> _itemList; //アイテムリスト
    private List<Text> _quantityTexts = new List<Text>(); // アイテム数表示用のテキストリスト

    private Action<ItemData> onItemSelected; // アイテム選択時のコールバック
    public ItemData SelectedItem => _selectedItem; //外部からどのスキルが選択されているか取得可能にする
    
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    // アイテムパネルの初期化（戦闘開始時に1回のみ呼び出す）
    public void InitializeItemPanel(List<ItemData> itemList, Action<ItemData> onItemSelectedCallback)
    {
        onItemSelected = onItemSelectedCallback;
        _itemList = itemList;

        // 既存のボタンを削除
        foreach (Transform child in _content)
        {
            Destroy(child.gameObject);
        }

        _itemButtons.Clear();
        _quantityTexts.Clear();

        // アイテムボタンを生成
        foreach (var item in _itemList)
        {
            var buttonObject = Instantiate(_itemButtonPrefab, _content);
            
            var button = buttonObject.GetComponent<Button>();
            var buttonText = buttonObject.GetComponentInChildren<Text>();

            buttonText.text = $"{item.Name} (x{item.Quantity})";
            button.onClick.AddListener(() => OnItemButtonClicked(item));

            _itemButtons.Add(button);
            _quantityTexts.Add(buttonText); // テキストの参照を保存
        }
    }

    // アイテムの個数を更新
    public void UpdateItemQuantities()
    {
        for (int i = 0; i < _itemList.Count; i++)
        {
            _quantityTexts[i].text = $"{_itemList[i].Name} (x{_itemList[i].Quantity})";
            // 個数が0ならボタンを無効化
            _itemButtons[i].interactable = _itemList[i].Quantity > 0;
        }
    }

    // ボタンがクリックされたときの処理
    private void OnItemButtonClicked(ItemData item)
    {
        Debug.Log($"Item {item.Name} selected.");
        onItemSelected?.Invoke(item); // コールバックを実行
        item.Quantity--;
    }
}
