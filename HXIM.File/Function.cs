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
                    return "�����������";

                Console.WriteLine("�������:{0}", input.ToJson());

                return EventMethod(input).ToJson();

            }
            catch (Exception e)
            {
                throw;
            }
        }

        /// <summary>  
        /// ����������еķ���
        /// </summary>  
        /// <param name="MethodName"></param>  
        /// <param name="Text"></param>  
        public object EventMethod(Model.Function func)
        {
            try
            {
                ////    1.Load(�����ռ�����)��GetType(�����ռ�.����)  
                Type type = Assembly.Load("HXIM.File.Event").GetType("HXIM.File.Event.{0}".FormatWith(func.Class));


                ////    2.GetMethod(��Ҫ���õķ�������)  
                MethodInfo method = type.GetMethod(func.Method);

                ParameterInfo[] paramsInfo = method.GetParameters();//�õ�ָ�������Ĳ����б�   

                if (func.Data.IsNull())
                    return null;

                //data������json�ַ���Ϊ׼
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
                ////    3.���õ�ʵ�����������Ǿ�̬��������Ҫ�������͵�һ��ʵ��  
                object obj = Activator.CreateInstance(type);
                ////    4.���÷�����������õ���һ����̬�������Ͳ���Ҫ��3�����������͵�ʵ����  
                ////      ��Ӧ�ص��þ�̬����ʱ��Invoke�ĵ�һ������Ϊnull  
                return method.Invoke(obj, parameters);
            }
            catch(Exception e)
            {
                return null;
            }
        }
    }
}
