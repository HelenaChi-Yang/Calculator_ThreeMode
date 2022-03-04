using Calculator.Operation.Operand;
using Calculator.Operation.Operators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.UpdateScreenState
{
    /// <summary>
    /// 按下運算子代表數字輸入完並進運算式後，新的一輪等待數字輸入狀態
    /// </summary>
    public class NewNumberState : ScreenResultState
    {
        /// <summary>
        /// Singleton實體
        /// </summary>
        private static NewNumberState Instance;

        /// <summary>
        /// 私有建構子(Singleton)
        /// </summary>
        private NewNumberState()
        {

        }

        /// <summary>
        /// 取得Singleton實體
        /// </summary>
        /// <returns></returns>
        public static NewNumberState GetInstance()
        {
            if (Instance == null)
            {
                Instance = new NewNumberState();
            }
            return Instance;
        }

        /// <summary>
        /// 按下 =
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressEqual(TheCalculator calculator, string buttonSign)
        {
            calculator.OperationsList.Add(new Number(calculator.ScreenNumber));
            calculator.ScreenResultState = NewResultState.GetInstance();
        }

        /// <summary>
        /// 按.
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressDot(TheCalculator calculator, string buttonSign)
        {
            calculator.Number = new StringBuilder(ButtonSign.ZERO_SIGN);
            calculator.Number.Append(buttonSign);
            calculator.ScreenNumber = calculator.Number.ToString();
            calculator.ScreenResultState = NormalResultState.GetInstance();
        }

        /// <summary>
        /// 按 (
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressLeftParenthesis(TheCalculator calculator, string buttonSign)
        {

        }

        /// <summary>
        /// 按數字鍵
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressNumber(TheCalculator calculator, string buttonSign)
        {
            OperateNumberLogic(calculator, buttonSign);

            calculator.ScreenResultState = NormalResultState.GetInstance();
        }

        /// <summary>
        /// 按運算子(此狀態下代表要更換運算子)
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressOperators(TheCalculator calculator, string buttonSign)
        {
            OperatorFactory operatorFactory = new OperatorFactory();
            calculator.OperationsList.RemoveAt(calculator.OperationsList.Count - 1);
            calculator.OperationsList.Add(operatorFactory.CreateOperator(buttonSign));
        }

        /// <summary>
        /// 按 +/-
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressPositiveAndNegative(TheCalculator calculator, string buttonSign)
        {
            calculator.Number = new StringBuilder(calculator.ScreenNumber);
            OperatePositiveAndNegativeLogic(calculator,  buttonSign);
            calculator.ScreenResultState = NormalResultState.GetInstance();
        }

        /// <summary>
        /// 按 )
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressRightParenthesis(TheCalculator calculator, string buttonSign)
        {

        }

        /// <summary>
        /// 按根號
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressSquare(TheCalculator calculator, string buttonSign)
        {
            calculator.Number = new StringBuilder(calculator.ScreenNumber);
            OperateSquareRootLogic(calculator, buttonSign);
            calculator.ScreenResultState = NormalResultState.GetInstance();
        }
    }
}
