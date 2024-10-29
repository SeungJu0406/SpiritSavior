using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disposable : MonoBehaviour
{
    bool _isDisposable;
    
    /// <summary>
    /// 일회용 bool 값 세팅
    /// </summary>
    /// <param name="value"></param>
    public void SetIsDisposable(bool value)
    {
        _isDisposable = value;
    }

    /// <summary>
    /// 게임에서 삭제 X
    /// </summary>
    public void ActiveTrap()
    {
        if (SceneChanger.Instance == null) return;
        SceneChanger.Instance.SetKeepingTrap(transform.position, true);
    }

    /// <summary>
    /// 게임에서 삭제
    /// </summary>
    public void UnActiveTrap()
    {
        if (SceneChanger.Instance == null) return;
        SceneChanger.Instance.SetKeepingTrap(transform.position, false);
    }
}
