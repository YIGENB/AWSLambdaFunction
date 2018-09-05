using RX.Web;
using RX;
using System;
using System.Collections.Generic;
using System.Text;

namespace HXIM.File.Model
{
    public class CommonErrorReturn : ICommonResult
    {
        public string errorCode { get; set; }
        public string errorMessage { get; set; }
        public bool success { get; set; }

        public CommonErrorReturn()
        {
            this.success = false;

        }

        /// <summary>
        /// 默认返回值 -1
        /// </summary>
        /// <param name="errorMsg"></param>
        public CommonErrorReturn(string errorMsg)
        {
            this.errorMessage = errorMsg;
            if (errorMsg.Contains("用户会话已经过期，请重新登录"))
            {
                this.errorCode = "-100";
            }
            else
            {
                this.errorCode = "-1";
            }

            this.success = false;

        }

        public CommonErrorReturn(string errorCode, string errorMsg)
        {
            this.errorMessage = errorMsg;
            this.errorCode = errorCode;
            this.success = false;
        }


        /// <summary>
        /// 把json字符串转换为通用的错误对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static CommonErrorReturn Parse(string json)
        {
            if (json.IsNull())
            {
                return null;

            }

            return json.FromJson<CommonErrorReturn>();
        }


        /// <summary>
        /// 返回json数据对象 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToJson();
        }



    }
}
