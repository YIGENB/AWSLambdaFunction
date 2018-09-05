using HXIM.File.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace HXIM.File.Event
{
    public class CommonHelper
    {
        /// <summary>
        /// 创建匿名的通用返回
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="AnonymousTypeObj"></param>
        /// <returns></returns>
        public static CommonResult<T> CreateCommonResponse<T>(T AnonymousTypeObj)
        {
            var commRlt = new CommonResult<T>(AnonymousTypeObj);
            return commRlt;
        }


        /// <summary>
        /// 默认返回值 -1
        /// </summary>
        /// <param name="errorMsg"></param>
        public static CommonErrorReturn CreateCommonError(string errorMsg)
        {
            return new CommonErrorReturn(errorMsg);
        }
    }
}
