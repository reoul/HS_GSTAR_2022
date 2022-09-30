using System;
using System.Collections;
using UnityEngine;

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

    public abstract void Roll();

    // 마우스나 터치로 해당 주사위를 잡았을 때
    private void OnMouseDown()
    {
        // 잡았으면 마우스나 터치 좌표로 이동시켜주는 코루틴 실행
        StartCoroutine(MoveCoroutine());
    }

    // 주사위에 대한 터치나 클릭을 끝냈을 때
    private void OnMouseUp()
    {
        // 이동시켜주는 코루틴 끝내기
        StopAllCoroutines();
        
        // 마우스 좌표에 카드가 있으면 카드 능력 실행
        int layerMask = 1 << LayerMask.NameToLayer("Card");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit, 1000, layerMask))
        {
            if (hit.collider.TryGetComponent(out Card card))
                card.Use(this);
            else
                Debug.LogError("레이어는 Card로 설정되어 있는데 Card 스크립트가 적용 안되어있음");
        }
    }

    // 주사위를 마우스 위치나 터치 위치로 이동시켜주는 코루틴
    IEnumerator MoveCoroutine()
    {
        int layerMask = ~(1 << LayerMask.NameToLayer("Dice"));
        while (true)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, 1000, layerMask))
            {
                transform.position = new Vector3(hit.point.x, hit.point.y + 0.8f, hit.point.z);
            }
            yield return null;
        }
    }
}
