using Calculator.BinaryExpressionTree;
using Calculator.Operation.Operand;
using Calculator.Operation.Operators;
using Calculator.Operation.OtherOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.UpdateScreenState
{
    /// <summary>
    /// 每個按鈕在不同狀態底下，會做出的動作與影響畫面呈現
    /// </summary>
    public abstract class ScreenResultState
    {
        /// <summary>
        /// 按等號
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public abstract void PressEqual(TheCalculator calculator, string buttonSign);

        /// <summary>
        /// 按數字鍵
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public abstract void PressNumber(TheCalculator calculator, string buttonSign);

        /// <summary>
        /// 按運算元
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public abstract void PressOperators(TheCalculator calculator, string buttonSign);

        /// <summary>
        /// 按左括號
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public abstract void PressLeftParenthesis(TheCalculator calculator, string buttonSign);

        /// <summary>
        /// 按右括號
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public abstract void PressRightParenthesis(TheCalculator calculator, string buttonSign);

        /// <summary>
        /// 按+-號
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public abstract void PressPositiveAndNegative(TheCalculator calculator, string buttonSign);

        /// <summary>
        /// 按根號
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public abstract void PressSquare(TheCalculator calculator, string buttonSign);

        /// <summary>
        /// 按 .
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public abstract void PressDot(TheCalculator calculator, string buttonSign);

        /// <summary>
        /// 處理根號相同運算邏輯
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void OperateSquareRootLogic(TheCalculator calculator, string buttonSign)
        {
            try
            {
                //開根號不能為負數
                if (Convert.ToDouble(calculator.Number.ToString()) >= 0)
                {
                    double result = Math.Sqrt(Convert.ToDouble(calculator.Number.ToString()));
                    calculator.Number = new StringBuilder(Convert.ToString(result));
                    calculator.ScreenNumber = calculator.Number.ToString();
                }
                else
                {
                    calculator.Number = new StringBuilder(string.Empty);
                    calculator.OperationsList = new List<Operations>();
                    calculator.Restart();
                    calculator.ErrorMessage = "無效輸入";
                }
            }
            catch (OverflowException)
            {
                calculator.Restart();
                calculator.ErrorMessage = "數值超出範圍";
                return;
            }
        }

        /// <summary>
        /// 處理 +/- 運算邏輯
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void OperatePositiveAndNegativeLogic(TheCalculator calculator, string buttonSign)
        {
            //number裡面不含負號且不為0
            if (!(calculator.Number.ToString().Contains(ButtonSign.SUBTRACTION_SIGN)) && !(calculator.Number.ToString().Equals(ButtonSign.ZERO_SIGN)))
            {
                calculator.Number.Insert(0, ButtonSign.SUBTRACTION_SIGN);
            }
            else if (!(calculator.Number.ToString().Equals(ButtonSign.ZERO_SIGN)))
            {
                calculator.Number.Remove(0, 1);
            }
            calculator.ScreenNumber = calculator.Number.ToString();
        }

        /// <summary>
        /// 處理數字按鈕邏輯
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void OperateNumberLogic(TheCalculator calculator, string buttonSign)
        {
            if (calculator.Number.ToString().Equals(ButtonSign.ZERO_SIGN))
            {
                calculator.Number.Remove(0, 1);
            }
            calculator.Number.Append(buttonSign);
            calculator.ScreenNumber = calculator.Number.ToString();
        }
    }
}
