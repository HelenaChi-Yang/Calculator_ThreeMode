using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ButtonHandler
{
    /// <summary>
    /// 根號
    /// </summary>
    public class SquareRoot : IButton
    {
        /// <summary>
        /// 根號運算
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void Process(TheCalculator calculator, string buttonSign)
        {
            calculator.ScreenResultState.PressSquare(calculator, buttonSign);
        }
    }
}
