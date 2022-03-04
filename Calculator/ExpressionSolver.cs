using Calculator.BinaryExpressionTree;
using Calculator.Operation.Operand;
using Calculator.Operation.Operators;
using Calculator.Operation.OtherOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class ExpressionSolver
    {
        /// <summary>
        /// 處理=邏輯
        /// </summary>
        /// <param name="calculator"></param>
        public void OperateEqualLogic(TheCalculator calculator)
        {
            AddRightParenthesis(calculator);
            if (!CheckDenominatorIsZero(calculator))
            {
                Tree tree = new Tree();
                Tree.Node expressionTree = tree.BuildExpressionTree(calculator.OperationsList);
                List<Operations> postorderOperation = tree.PostorderTraversal(expressionTree, new List<Operations>());
                decimal result = tree.PostfixResult(postorderOperation);
                //重置
                calculator.UpdateCalculator();
                //留下結果
                calculator.ScreenNumber = result.ToString();
            }
        }

        /// <summary>
        /// 修正運算式，補足()
        /// </summary>
        /// <param name="calculator">計算機</param>
        public void AddRightParenthesis(TheCalculator calculator)
        {
            while (calculator.CountParenthesis > CountRightParenthesis(calculator))
            {
                calculator.OperationsList.Add(new RightParenthesis());
            }
            //預設一個最低優先級+
            Operations beforeOperator = new Addition();
            Operations laterOperator = new Addition();
            foreach (Operations theOperator in calculator.OperationsList.ToArray())
            {
                if (theOperator.GetType().BaseType == typeof(Operator))
                {
                    laterOperator = theOperator;

                    //判斷運算子優先級
                    if (laterOperator.Priority > beforeOperator.Priority)
                    {
                        int index = calculator.OperationsList.IndexOf(theOperator);
                        calculator.OperationsList.Insert(index + 2, new RightParenthesis());
                        calculator.OperationsList.Insert(index - 1, new LeftParenthesis());
                    }
                    laterOperator = beforeOperator;
                }
            }
            calculator.OperationsList.Insert(0, new LeftParenthesis());
            calculator.OperationsList.Add(new RightParenthesis());
        }

        /// <summary>
        /// 算右括號數量
        /// </summary>
        /// <param name="calculator"></param>
        /// <returns></returns>
        public int CountRightParenthesis(TheCalculator calculator)
        {
            int numberOfRightParenthesis = 0;
            foreach (Operations temp in calculator.OperationsList)
            {
                if (temp.GetType() == typeof(RightParenthesis))
                {
                    numberOfRightParenthesis += 1;
                }
            }
            return numberOfRightParenthesis;
        }

        /// <summary>
        /// 檢查分母為0
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <returns></returns>
        public bool CheckDenominatorIsZero(TheCalculator calculator)
        {
            foreach (Operations AOperator in calculator.OperationsList)
            {
                if (AOperator.Sign.Equals(ButtonSign.DIVISION_SIGN))
                {
                    int index = calculator.OperationsList.IndexOf(AOperator);
                    if (calculator.OperationsList[index + 1].Sign.Equals(ButtonSign.ZERO_SIGN))
                    {
                        calculator.Number.Remove(0, calculator.Number.Length);
                        calculator.OperationsList.RemoveRange(0, calculator.OperationsList.Count);
                        calculator.ErrorMessage = "分母不可為0";
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
