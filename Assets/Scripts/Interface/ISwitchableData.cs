/// <summary>
/// エディター画面で素材の切り替えを行いやすくするためのインターフェースです
/// </summary>
public interface ISwitchableData<T>
{
    T GetItem(int index);
    int GetItemCount();
}
