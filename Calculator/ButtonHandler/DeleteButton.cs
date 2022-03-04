using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator.ButtonHandler
{
    /// <summary>
    /// 刪除鍵
    /// </summary>
    public class DeleteButton : IButton
    {
        /// <summary>
        /// 覆寫IButton
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void Process(TheCalculator calculator, string buttonSign)
        {
            if (!(calculator.Number.ToString().Equals(string.Empty)))
            {
                calculator.Number.Remove(calculator.Number.Length - 1, 1);
            }

            if (string.IsNullOrEmpty(calculator.Number.ToString()))
            {
                calculator.Number.Append(ButtonSign.ZERO_SIGN);
            }
            calculator.ScreenNumber = calculator.Number.ToString();
        }
    }
}
