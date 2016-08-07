using UnityEngine;
using System.Collections;

public class DelegateExample : MonoBehaviour {

    int num = 10;

    delegate int MyDelegate(int x);

    int AddNum(int n)
    {
        num += n;
        return num;
    }

    int MultiplyNum(int n)
    {
        num *= n;
        return num;
    }

    void Start()
    {
        MyDelegate del1 = new MyDelegate(AddNum);
        MyDelegate del2 = new MyDelegate(MultiplyNum);
        MyDelegate myMulticastDelegate = del1 + del2;

        del1(10);

        print(num);

        del2(10);

        print(num);

        num = 10;

        myMulticastDelegate(10);

        print(num);
    }
}
