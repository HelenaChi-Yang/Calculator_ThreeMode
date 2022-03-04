using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    /// <summary>
    /// 產生類別路徑工廠
    /// </summary>
    public class ClassNameFactory
    {
        /// <summary>
        /// 紀錄所有類別名稱
        /// </summary>
        Dictionary<string, string> ClassNameOfSign;

        /// <summary>
        /// 建構子
        /// </summary>
        public ClassNameFactory()
        {
            ClassNameOfSign = new Dictionary<string, string>
            {
                {"+", "Calculator.Operation.Operators.Addition"},
                {"-", "Calculator.Operation.Operators.Subtraction"},
                {"*", "Calculator.Operation.Operators.Multiplication"},
                {"/", "Calculator.Operation.Operators.Division"},
                {"C", "Calculator.ButtonHandler.ClearButton"},
                {"CE", "Calculator.ButtonHandler.ClearErrorButton"},
                {"back", "Calculator.ButtonHandler.DeleteButton"},
                {"+/-", "Calculator.ButtonHandler.PositiveAndNegativeButton"},
                {"(", "Calculator.Operation.OtherOperators.LeftParenthesis"},
                {")", "Calculator.Operation.OtherOperators.RightParenthesis"},
                {"√", "Calculator.ButtonHandler.SquareRoot"},
                {".", "Calculator.ButtonHandler.DotButton"},
                {"=", "Calculator.ButtonHandler.EqualButton"},
                {"0", "Calculator.ButtonHandler.NumberButton"},
                {"1", "Calculator.ButtonHandler.NumberButton"},
                {"2", "Calculator.ButtonHandler.NumberButton"},
                {"3", "Calculator.ButtonHandler.NumberButton"},
                {"4", "Calculator.ButtonHandler.NumberButton"},
                {"5", "Calculator.ButtonHandler.NumberButton"},
                {"6", "Calculator.ButtonHandler.NumberButton"},
                {"7", "Calculator.ButtonHandler.NumberButton"},
                {"8", "Calculator.ButtonHandler.NumberButton"},
                {"9", "Calculator.ButtonHandler.NumberButton"}
            };
        }

        /// <summary>
        /// 取得類別名稱
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        public string GetClassNmae(string sign)
        {
            if(ClassNameOfSign.TryGetValue(sign, out string result))
            {
                return result;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
