using Calculator.ButtonHandler;
using Calculator.Operation.Operand;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Calculator.Operation.Operators
{
    /// <summary>
    /// 運算子
    /// </summary>
    public abstract class Operator : Operations, IButton
    {
        /// <summary>
        /// 運算
        /// </summary>
        /// <param name="firstOperand">第一運算元</param>
        /// <param name="secondOperand">第二運算元</param>
        /// <returns></returns>
        public abstract decimal Calculate(decimal firstOperand, decimal secondOperand);

        /// <summary>
        /// 覆寫IButton
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void Process(TheCalculator calculator, string buttonSign)
        {
            calculator.ScreenResultState.PressOperators(calculator, buttonSign);
        }
    }
}
