using Calculator;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API
{
    public class CalculatorAndScreen
    {
        /// <summary>
        /// 十台計算機
        /// </summary>
        public ConcurrentDictionary<string, TheCalculator> TenCalculator { get; set; }

        /// <summary>
        /// 建構子
        /// </summary>
        public CalculatorAndScreen()
        {
            TenCalculator = new ConcurrentDictionary<string, TheCalculator>(5, 10);
        }
    }
}
