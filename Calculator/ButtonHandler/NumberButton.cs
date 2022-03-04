using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.ButtonHandler
{
    /// <summary>
    /// 數字鍵
    /// </summary>
    public class NumberButton : IButton
    {
        /// <summary>
        /// 運算
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void Process(TheCalculator calculator, string buttonSign)
        {
            calculator.ScreenResultState.PressNumber(calculator, buttonSign);
        }
    }
}
