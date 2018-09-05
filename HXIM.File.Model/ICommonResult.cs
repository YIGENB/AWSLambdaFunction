using System;
using System.Collections.Generic;
using System.Text;

namespace HXIM.File.Model
{
    public interface ICommonResult
    {

        string errorCode { get; set; }
        string errorMessage { get; set; }
        bool success { get; set; }

        string ToString();

    }
}
