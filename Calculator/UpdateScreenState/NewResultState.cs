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
    /// 有新運算結果時，按鈕需做不同動作
    /// </summary>
    public class NewResultState : ScreenResultState
    {
        /// <summary>
        /// Singleton實體
        /// </summary>
        private static NewResultState Instance;

        /// <summary>
        /// 私有建構子(Singleton)
        /// </summary>
        private NewResultState()
        {

        }

        /// <summary>
        /// 取得Singleton實體
        /// </summary>
        /// <returns></returns>
        public static NewResultState GetInstance()
        {
            if (Instance == null)
            {
                Instance = new NewResultState();
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
            return;
        }

        /// <summary>
        /// 按.
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressDot(TheCalculator calculator, string buttonSign)
        {
            calculator.Number = new StringBuilder(calculator.ScreenNumber);
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
            calculator.ScreenResultState = NormalResultState.GetInstance();
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
        /// 按運算子
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressOperators(TheCalculator calculator, string buttonSign)
        {
            //歷史結果繼續用
            calculator.OperationsList.Add(new Number(calculator.ScreenNumber));

            //加入運算子
            OperatorFactory operatorFactory = new OperatorFactory();
            calculator.OperationsList.Add(operatorFactory.CreateOperator(buttonSign));

            calculator.ScreenResultState = NormalResultState.GetInstance();
        }

        /// <summary>
        /// 按 +/-
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressPositiveAndNegative(TheCalculator calculator, string buttonSign)
        {
            //歷史結果繼續用
            calculator.Number = new StringBuilder(calculator.ScreenNumber);

            OperatePositiveAndNegativeLogic(calculator, buttonSign);

            calculator.ScreenResultState = NormalResultState.GetInstance();
        }

        /// <summary>
        /// 按 )
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressRightParenthesis(TheCalculator calculator, string buttonSign)
        {
            calculator.ScreenResultState = NormalResultState.GetInstance();
        }

        /// <summary>
        /// 按根號
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressSquare(TheCalculator calculator, string buttonSign)
        {
            //歷史數字繼續用
            calculator.Number = new StringBuilder(calculator.ScreenNumber);

            OperateSquareRootLogic(calculator, buttonSign);

            calculator.ScreenResultState = NormalResultState.GetInstance();
        }
    }
}
