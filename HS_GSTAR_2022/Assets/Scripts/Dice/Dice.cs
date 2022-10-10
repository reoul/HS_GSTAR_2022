using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

public enum EDiceNumber
{
    One = 1,
    Two,
    Three,
    Four,
    Five,
    Six,
    Max
}

public abstract class Dice : MonoBehaviour
{
    public EDiceNumber Number { get; protected set; }
    private bool _canMoveToMouse;

    private void Start()
    {
        _canMoveToMouse = false;
        
        Roll();
    }
    
    /// <summary> 주사위 굴리기 </summary>
    public abstract void Roll();

    /// <summary> 마우스나 터치로 해당 주사위를 잡았을 때 </summary>
    private void OnMouseDown()
    {
        // 잡았으면 마우스나 터치 좌표로 이동시켜주는 코루틴 실행
        _canMoveToMouse = true;
        StartCoroutine(MoveCoroutine());
    }

    /// <summary> 주사위에 대한 터치나 클릭을 끝냈을 때 </summary>
    private void OnMouseUp()
    {
        // 이동시켜주는 코루틴 끝내기
        _canMoveToMouse = false;
        
        Logger.Assert(Camera.main != null);
        // 마우스 좌표에 카드가 있으면 카드 능력 실행
        int layerMask = 1 << LayerMask.NameToLayer("Card");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 1000, layerMask))
        {
            if (hit.collider.TryGetComponent(out Card card))
            {
                card.Use(this);             // 카드 사용하고
                Destroy(this.gameObject);       // 주사위 삭제
            }
            else
                Logger.LogError("레이어는 Card로 설정되어 있는데 Card 스크립트가 적용 안되어있음");
        }
    }

    /// <summary> 주사위를 마우스 위치나 터치 위치로 이동시켜주는 코루틴 </summary>
    IEnumerator MoveCoroutine()
    {
        Logger.Assert(Camera.main != null);
        int layerMask = ~(1 << LayerMask.NameToLayer("Dice"));
        while (_canMoveToMouse)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000, layerMask))
            {
                transform.position = new Vector3(hit.point.x, hit.point.y + 1.1f, hit.point.z);     // 주사위 바닥에서 살짝 위로 위치 고정
            }
            yield return null;
        }
    }
}
