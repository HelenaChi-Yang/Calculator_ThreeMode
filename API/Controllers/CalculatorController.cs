using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Calculator;
using API.Models;
using System.Collections.Concurrent;

namespace API.Controllers
{
    /// </summary>
    /// 計算機控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        /// <summary>
        /// 十台計算機
        /// </summary>
        private CalculatorAndScreen Calculator;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="calculatorAndScreen">計算機</param>
        public CalculatorController(CalculatorAndScreen calculatorAndScreen)
        {
            Calculator = calculatorAndScreen;
        }

        /// <summary>
        /// 取得計算機當前運算式，和運算元
        /// </summary>
        /// <param name="sign">輸入符號</param>
        /// <returns></returns>       
        [HttpGet("{id}")]
        public TwoResultOnScreen Get(string sign, string id)
        {
            TwoResultOnScreen twoResultOnScreen = new TwoResultOnScreen();
            TheCalculator calculator = GetCalculator(id);
            string className = calculator.GetButtonClassName(sign);
            if (string.IsNullOrEmpty(className))
            {
                twoResultOnScreen.ExpressionResult = "請輸入正確運算子或運算符";
                return twoResultOnScreen;
            }

            calculator.ClickButton(className, sign);

            twoResultOnScreen.OperandResult = calculator.CreateOperand(calculator);
            twoResultOnScreen.ExpressionResult = calculator.CreateExpression(calculator.OperationsList);
            return twoResultOnScreen;
        }

        /// <summary>
        /// 取得計算機
        /// </summary>
        /// <param name="id">編號</param>
        /// <returns></returns>
        private TheCalculator GetCalculator(string id)
        {
            TheCalculator calculator;
            if (Calculator.TenCalculator.TryGetValue(id, out calculator))
            {
                return calculator;
            }
            calculator = new TheCalculator();
            Calculator.TenCalculator.TryAdd(id, calculator);
            return calculator;
        }
    }
}
