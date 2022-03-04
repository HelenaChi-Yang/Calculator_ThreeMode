using Calculator.ButtonHandler;
using Calculator.UpdateScreenState;
using System;
using System.Collections.Generic;
using System.Text;

namespace Calculator
{
    /// <summary>
    /// 計算機運算邏輯
    /// </summary>
    public class TheCalculator
    {
        /// <summary>
        /// 所有數字
        /// </summary>
        public StringBuilder Number { get; set; }

        /// <summary>
        /// 儲存運算式
        /// </summary>
        public List<Operations> OperationsList { get; set; }

        /// <summary>
        /// 螢幕目前數字
        /// </summary>
        public string ScreenNumber { get; set; }

        /// <summary>
        /// 錯誤訊息
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// 畫面結果狀態
        /// </summary>
        public ScreenResultState ScreenResultState { get; set; }

        /// <summary>
        /// 計算括號數量
        /// </summary>
        public int CountParenthesis { get; set; }

        /// <summary>
        /// 運算式處理器
        /// </summary>
        public ExpressionSolver ExpressionSolver { get; set; }

        /// <summary>
        /// 建構子
        /// </summary>
        public TheCalculator()
        {
            OperationsList = new List<Operations>();
            Number = new StringBuilder("0");
            ScreenResultState = NormalResultState.GetInstance();
        }

        /// <summary>
        /// 根據按鈕做動作(處理運算邏輯)
        /// </summary>
        /// <param name="buttonClassName">按鈕對應的class名稱</param>
        /// <param name="buttonSign">按鈕符號</param>
        public void ClickButton(string buttonClassName, string buttonSign)
        {
            string className = buttonClassName;
            IButton button = (IButton)Activator.CreateInstance(Type.GetType(className));
            button.Process(this, buttonSign);
            ExpressionSolver = new ExpressionSolver();
        }

        /// <summary>
        /// 結果生成後，重置計算機
        /// </summary>
        public void UpdateCalculator()
        {
            OperationsList = new List<Operations>();
            Number = new StringBuilder(ButtonSign.ZERO_SIGN);
            ErrorMessage = string.Empty;
            CountParenthesis = 0;
        }

        /// <summary>
        /// 計算機全部重置
        /// </summary>
        public void Restart()
        {
            OperationsList = new List<Operations>();
            Number = new StringBuilder(ButtonSign.ZERO_SIGN);
            ScreenResultState = NormalResultState.GetInstance();
            ErrorMessage = string.Empty;
            CountParenthesis = 0;
            ScreenNumber = ButtonSign.ZERO_SIGN;
        }

        /// <summary>
        /// 取的 Class路徑
        /// </summary>
        /// <param name="sign"></param>
        /// <returns></returns>
        public string GetButtonClassName(string sign)
        {
            ClassNameFactory classNameFactory = new ClassNameFactory();
            string result = classNameFactory.GetClassNmae(sign);
            return result;
        }

        /// <summary>
        /// 控制目前文字
        /// </summary>
        /// <returns></returns>
        public string CreateOperand(TheCalculator theCalculator)
        {
            if (string.IsNullOrEmpty(theCalculator.ErrorMessage))
            {
                return theCalculator.ScreenNumber;
            }
            string tempErrorMessage = theCalculator.ErrorMessage;
            theCalculator.Restart();
            return tempErrorMessage;
        }

        /// <summary>
        /// 控制算式文字
        /// </summary>
        /// <returns></returns>
        public string CreateExpression(List<Operations> operationsList)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (Operations operation in operationsList)
            {
                stringBuilder.Append(operation.Sign);
            }
            return stringBuilder.ToString();
        }
    }
}
