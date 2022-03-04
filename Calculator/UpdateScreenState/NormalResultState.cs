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
    /// 一般輸入狀態
    /// </summary>
    public class NormalResultState : ScreenResultState
    {
        /// <summary>
        /// Singleton實體
        /// </summary>
        private static NormalResultState Instance;

        /// <summary>
        /// 私有建構子(Singleton)
        /// </summary>
        private NormalResultState()
        {

        }

        /// <summary>
        /// 取得Singleton實體
        /// </summary>
        /// <returns></returns>
        public static NormalResultState GetInstance()
        {
            if(Instance == null)
            {
                Instance = new NormalResultState();
            }
            return Instance;
        }

        /// <summary>
        /// 按 =
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressEqual(TheCalculator calculator, string buttonSign)
        {
            //修正運算式，若最後沒有輸入運算元，加上screenNumber
            if (!(string.IsNullOrEmpty(calculator.Number.ToString())))
            {
                calculator.OperationsList.Add(new Number(calculator.Number.ToString()));
                calculator.ScreenNumber = calculator.Number.ToString();
                calculator.Number.Clear();
            }
            else
            {
                for (int i = calculator.OperationsList.Count - 1; i >= 0; i--)
                {
                    if (calculator.OperationsList[i].GetType() != typeof(RightParenthesis))
                    {
                        if (calculator.OperationsList[i].GetType() != typeof(Number))
                        {
                            calculator.OperationsList.Insert(i + 1, new Number(calculator.ScreenNumber));
                        }
                        break;
                    }
                }
            }

            calculator.ScreenResultState = NewResultState.GetInstance();
        }

        /// <summary>
        /// 按.
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressDot(TheCalculator calculator, string buttonSign)
        {
            //若number為空，起始加上0
            if ((string.IsNullOrEmpty(calculator.Number.ToString())))
            {
                calculator.Number = new StringBuilder(ButtonSign.ZERO_SIGN);
            }

            //若沒有.才加
            if (!(calculator.Number.ToString().Contains(ButtonSign.DOT_SIGN)))
            {
                calculator.Number.Append(buttonSign);
            }

            calculator.ScreenNumber = calculator.Number.ToString();
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
        }

        /// <summary>
        /// 按運算元
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressOperators(TheCalculator calculator, string buttonSign)
        {
            OperatorFactory operatorFactory = new OperatorFactory();
            if (!(string.IsNullOrEmpty(calculator.Number.ToString())))
            {
                calculator.OperationsList.Add(new Number(calculator.Number.ToString()));
                calculator.ScreenNumber = calculator.Number.ToString();
                calculator.Number.Clear();
            }


            calculator.OperationsList.Add(operatorFactory.CreateOperator(buttonSign));

            calculator.ScreenResultState = NewNumberState.GetInstance();
        }

        /// <summary>
        /// 按 +/-
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        public override void PressPositiveAndNegative(TheCalculator calculator, string buttonSign)
        {
            OperatePositiveAndNegativeLogic(calculator, buttonSign);
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
            OperateSquareRootLogic(calculator, buttonSign);
        }
    }
}
