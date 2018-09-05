using System;
using System.Collections.Generic;
using System.Text;

namespace HXIM.File.Model
{
    //用于处理lambda函数
    public class Function : IFunction
    {
        /// <summary>
        /// 类名
        /// </summary>
        public string Class { get; set; }
        /// <summary>
        /// 函数名
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
