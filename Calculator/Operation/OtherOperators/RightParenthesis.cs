using Calculator.ButtonHandler;
using Calculator.Operation.Operand;
using Calculator.Operation.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Operation.OtherOperators
{
    public class RightParenthesis : Operations, IButton
    {
        public RightParenthesis()
        {
            Sign = ButtonSign.RIGHTPARENTHESIS_SIGN;
            Priority = 0;
        }

        public void Process(TheCalculator calculator, string buttonSign)
        {
            //判斷 )數量有無超過 (
            int numberOfRightParenthese = CountRightParentheseNumber(calculator);
            if (calculator.CountParenthesis > numberOfRightParenthese)
            {
                //檢查number是否有數字
                if (!(string.IsNullOrEmpty(calculator.Number.ToString())))
                {
                    calculator.OperationsList.Add(new Number(calculator.Number.ToString()));
                    calculator.ScreenNumber = calculator.Number.ToString();
                    calculator.Number = new StringBuilder();
                }
                int mustHaveFourOperationBeforeRightParenthese = 4;
                if (calculator.OperationsList.Count >= mustHaveFourOperationBeforeRightParenthese)
                {
                    //上一個輸入數字以及運算子(EX：+5, -4, *6, /2)，或 )，可以輸入 )
                    Type lastOperation = calculator.OperationsList[calculator.OperationsList.Count - 1].GetType();
                    Type theSecondToLast = calculator.OperationsList[calculator.OperationsList.Count - 2].GetType();
                    if ((lastOperation == typeof(Number) && theSecondToLast.BaseType == typeof(Operator)) || lastOperation == typeof(RightParenthesis))
                    {
                        calculator.OperationsList.Add(new RightParenthesis());
                    }
                }
            }
        }

        public int CountRightParentheseNumber(TheCalculator calculator)
        {
            int numberOfRightParenthese = 0;
            foreach (Operations operation in calculator.OperationsList)
            {
                if (operation.GetType() == typeof(RightParenthesis))
                {
                    numberOfRightParenthese += 1;
                }
            }
            return numberOfRightParenthese;
        }
    }
}
