using System.Collections.Generic;

public interface IUseCard
{
    /// <summary> 가지고 있는 카드들 코드를 가져옴 </summary>
    /// <returns>가지고 있는 카드들 코드</returns>
    List<string> GetCardCodes();
}
