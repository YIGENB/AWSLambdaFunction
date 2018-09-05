using System;
using System.Reflection;

using Amazon.Lambda.Core;
using RX;
using RX.Web;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace HXIM.File
{
    public class Function
    {
        /// <summary>
        /// This method is called for every Lambda invocation. This method takes in an S3 event object and can be used 
        /// to respond to S3 notifications.
        /// </summary>
        /// <param name="evnt"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public string FunctionHandler(Model.Function input, ILambdaContext context)
        {
            try
            {
                if (input.IsNull())
                    return "请求参数错误";

                Console.WriteLine("输入参数:{0}", input.ToJson());

                return EventMethod(input).ToJson();

            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>  
        /// 反射调用类中的方法
        /// </summary>  
        /// <param name="MethodName"></param>  
        /// <param name="Text"></param>  
        public object EventMethod(Model.Function func)
        {
            try
            {
                ////    1.Load(命名空间名称)，GetType(命名空间.类名)  
                Type type = Assembly.Load("HXIM.File.Event").GetType("HXIM.File.Event.{0}".FormatWith(func.Class));


                ////    2.GetMethod(需要调用的方法名称)  
                MethodInfo method = type.GetMethod(func.Method);

                ParameterInfo[] paramsInfo = method.GetParameters();//得到指定方法的参数列表   

                if (func.Data.IsNull())
                    return null;

                //data参数以json字符串为准
                var jObj = Newtonsoft.Json.Linq.JObject.FromObject(func.Data);

                object[] parameters = new object[paramsInfo.Length];

                for (int i = 0; i < paramsInfo.Length; i++)
                {
                    Type tType = paramsInfo[i].ParameterType;

                    if (tType.Equals(typeof(string)) || (!tType.IsInterface && !tType.IsClass))
                        parameters[i] = jObj.Value<string>(paramsInfo[i].Name);
                    else if (tType.Equals(typeof(int)) || (!tType.IsInterface && !tType.IsClass))
                        parameters[i] = jObj.Value<int>(paramsInfo[i].Name);
                    else if (tType.Equals(typeof(long)) || (!tType.IsInterface && !tType.IsClass))
                        parameters[i] = jObj.Value<long>(paramsInfo[i].Name);
                    else if (tType.Equals(typeof(float)) || (!tType.IsInterface && !tType.IsClass))
                        parameters[i] = jObj.Value<float>(paramsInfo[i].Name);
                    else if (tType.Equals(typeof(decimal)) || (!tType.IsInterface && !tType.IsClass))
                        parameters[i] = jObj.Value<decimal>(paramsInfo[i].Name);
                    else if (tType.Equals(typeof(bool)) || (!tType.IsInterface && !tType.IsClass))
                        parameters[i] = jObj.Value<bool>(paramsInfo[i].Name);
                }
                ////    3.调用的实例化方法（非静态方法）需要创建类型的一个实例  
                object obj = Activator.CreateInstance(type);
                ////    4.调用方法，如果调用的是一个静态方法，就不需要第3步（创建类型的实例）  
                ////      相应地调用静态方法时，Invoke的第一个参数为null  
                return method.Invoke(obj, parameters);
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
