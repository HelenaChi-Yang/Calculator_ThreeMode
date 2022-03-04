using Calculator.ButtonHandler;
using Calculator.Operation.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operation.OtherOperators
{
    public class LeftParenthesis : Operations, IButton
    {
        /// <summary>
        /// 建構子
        /// </summary>
        public LeftParenthesis()
        {
            Sign = ButtonSign.LEFTPARENTHESIS_SIGN;
        }

        /// <summary>
        /// 實作IButton
        /// </summary>
        /// <param name="calculatorOperation">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void Process(TheCalculator calculator, string buttonSign)
        {
            //上一個輸入運算子(+-*/)，不給輸入左括號
            if (calculator.OperationsList.Count > 0)
            {
                //若前面為運算子 或 ( 可加 (
                Type lastType = calculator.OperationsList[calculator.OperationsList.Count - 1].GetType();
                if (lastType.BaseType == typeof(Operator) || lastType == typeof(LeftParenthesis))
                {
                    calculator.OperationsList.Add(new LeftParenthesis());
                    calculator.CountParenthesis += 1;
                }
            }
            else if (calculator.OperationsList.Count == 0)
            {
                calculator.OperationsList.Add(new LeftParenthesis());
                calculator.CountParenthesis += 1;
            }
        }
    }
}
