﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemiTransparentImgDisplay.Services.Serialization
{
    public interface ISerializer
    {
        string Serialize(object obj);

        T Deserialize<T>(string data);
    }
}
