using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Xunit;
using Amazon.Lambda;
using Amazon.Lambda.Core;
using Amazon.Lambda.TestUtilities;
using Amazon.Lambda.S3Events;

using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Util;

using RX.Web;

namespace HXIM.File.Tests
{
    public class FunctionTest
    {
        [Fact]
        public void TestS3EventLambdaFunction()
        {
            try
            {
                //var model = new Model.Function()
                //{
                //    Class= "AWSS3",
                //    Method= "UploadFile",
                //    Data= "{\"keyName\":\"targetList\",\"resimPath\":\"C:\\\\Users\\\\Administrator\\\\Desktop\\\\Dec_All\\\\Web\\\\Dec.WebIIS\\\\abc.html\"}"
                //};

                //var aaa = model.ToJson();


                var input = "{\"Class\":\"AWSS3\",\"Method\":\"UploadFile\",\"Data\":{\"keyName\":\"targetList\",\"resimPath\":\"2222\"}}";

                var func = input.FromJson<Model.Function>();


                var res= new Function().EventMethod(func).ToJson();
                
                Assert.NotNull(res);

            }
            finally
            {
                // Clean up the test data
            }
        }
    }
}
