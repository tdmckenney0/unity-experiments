using UnityEngine;
using System.Collections;

public class Calculator : MonoBehaviour {

    public int firstOperand;
    public int secondOperand;

    public int add()
    {
        return (firstOperand + secondOperand);
    }
}
