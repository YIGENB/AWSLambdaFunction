using RX.Web;
using System;
using System.Collections.Generic;
using System.Text;

namespace HXIM.File.Model
{
    /// <summary>
    /// 通用的返回对象 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonResult<T> : ICommonResult
    {

        public string errorMessage { get; set; }
        public string errorCode { get; set; }
        public bool success { get; set; }



        public T data { get; set; }
        public int recordCount { get; set; }
        public CommonResult(T Data)
        {
            this.data = Data;
            this.success = true;

        }



        //public CommonResult<T>(T)


        /// <summary>
        /// 返回json字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToJson();
        }



        /// <summary>
        /// 把当前服务器的返回值转换为错误对象 
        /// </summary>
        /// <returns>返回 CommonErrorReturn 实例，当success为true 的时候表示的没有错误返回 </returns>
        public CommonErrorReturn ConvertToErrorReturn()
        {
            if (this.success)
            {
                CommonErrorReturn errorRlt = new CommonErrorReturn();
                errorRlt.success = true;
                errorRlt.errorMessage = "";
                errorRlt.errorCode = "";
                return errorRlt;
            }
            else
            {
                CommonErrorReturn errorRlt = new CommonErrorReturn(this.errorMessage);
                return errorRlt;
            }

        }
    }
}
