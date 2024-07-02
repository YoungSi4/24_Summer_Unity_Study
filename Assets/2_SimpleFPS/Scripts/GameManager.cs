using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas canvas; // 일단 캔버스를 들고 있어야함
    public Transform canvsTransform;
    
    // 이 스크립트에 접근할 방법이 필요한데, 스파게티 주범이니 최대한 자제할 것
    // 자기 자신을 스태틱 변수로 다른 코드에 제공하겠다는 뜻
    // Vector3.zero 같은 애들도 static으로 선언되었기 때문에 별도로 참조 없이도 접근 가능하다.
    public static GameManager instance = null;

    private void Start()
    {
        // throw로 예외처리를 하고 작성하기도 한다.
        instance = this;
    }


}
