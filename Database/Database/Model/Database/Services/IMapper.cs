using Database.Model.Database.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Model.Database.Services
{
    interface IMapper<T> where T:class
    {
        void Create(T obj);
        void Update(T obj);
        void Delete(T[] obj);
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();  
    }
}
