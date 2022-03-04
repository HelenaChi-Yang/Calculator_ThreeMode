using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.ButtonHandler
{
    /// <summary>
    /// button 的抽象物件，提供 button 處理按鈕
    /// </summary>
    public interface IButton
    {
        /// <summary>
        /// 按鈕執行過程
        /// </summary>
        /// <param name="calculator">計算機</param>
        /// <param name="buttonSign">按鈕符號</param>
        void Process(TheCalculator calculator, string buttonSign);
    }
}
