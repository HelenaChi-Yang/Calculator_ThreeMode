using Calculator.BinaryExpressionTree;
using Calculator.Operation.Operand;
using Calculator.Operation.Operators;
using Calculator.Operation.OtherOperators;
using Calculator.UpdateScreenState;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.ButtonHandler
{
    /// <summary>
    /// 等號
    /// </summary>
    public class EqualButton : IButton
    {
        /// <summary>
        /// 覆寫IButton
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void Process(TheCalculator calculator, string buttonSign)
        {
            calculator.ScreenResultState.PressEqual(calculator, buttonSign);
            calculator.ExpressionSolver.OperateEqualLogic(calculator);
        }
    }
}
