using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Canvas canvas; // �ϴ� ĵ������ ��� �־����
    public Transform canvsTransform;
    
    // �� ��ũ��Ʈ�� ������ ����� �ʿ��ѵ�, ���İ�Ƽ �ֹ��̴� �ִ��� ������ ��
    // �ڱ� �ڽ��� ����ƽ ������ �ٸ� �ڵ忡 �����ϰڴٴ� ��
    // Vector3.zero ���� �ֵ鵵 static���� ����Ǿ��� ������ ������ ���� ���̵� ���� �����ϴ�.
    public static GameManager instance = null;

    private void Start()
    {
        // throw�� ����ó���� �ϰ� �ۼ��ϱ⵵ �Ѵ�.
        instance = this;
    }


}
