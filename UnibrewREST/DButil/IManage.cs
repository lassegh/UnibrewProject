using System.Collections.Generic;

namespace UnibrewREST
{
    public interface IManage<T>
    {
        IEnumerable<T> Get();
        T Get(int id);
        bool Post(T elem);
        bool Put(int id, T elem);
        bool Delete(int id);
    }
}