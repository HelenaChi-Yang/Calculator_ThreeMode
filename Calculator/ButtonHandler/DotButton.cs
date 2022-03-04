using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.ButtonHandler
{
    /// <summary>
    /// 小數點鍵
    /// </summary>
    public class DotButton : IButton
    {
        /// <summary>
        /// 覆寫IButton
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void Process(TheCalculator calculator, string buttonSign)
        {
            calculator.ScreenResultState.PressDot(calculator, buttonSign);
        }
    }
}
