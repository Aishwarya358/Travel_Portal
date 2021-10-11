using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelRepository
{
    public interface ITravel<T> where T : class
    {
        IEnumerable<T> Getall();
        T getbyid(object id);
        T update(T data);
        T add(T data);

    }
}
