using System;
using System.Collections.Generic;
using System.Linq;
using RunPath.WebApi.Domain;
using RunPath.WebApi.Services.Interfaces;

namespace RunPath.WebApi.Services.Interfaces
{
    public interface IHttpDataReaderService<T>
    {
        List<T> ReadAsList(string url);
        T ReadAsItem(string url);
    }
}
