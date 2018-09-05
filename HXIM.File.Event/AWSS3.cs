using System;
using System.Collections.Generic;
using System.Text;

namespace HXIM.File.Event
{
    public class AWSS3
    {
        static HXIM.File.AWSS3.AWSS3 S3Client { get; set; }
        public AWSS3()
        {
            S3Client = new HXIM.File.AWSS3.AWSS3();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="keyName"></param>
        /// <param name="resimPath"></param>
        public static object UploadFile(string keyName,string resimPath)
        {
            try
            {
                S3Client.WritingAnObjectAsync(keyName, resimPath).Wait();


                var data = CommonHelper.CreateCommonResponse(new
                {
                   Message="OK"
                });

                data.success = true;

                return data;

            }
            catch(Exception e)
            {
                return CommonHelper.CreateCommonError(e.Message);

            }

        }
    }
}
