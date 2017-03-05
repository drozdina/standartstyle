using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Standartstyle.App_Code.Util
{
    interface IRepository<T> where T : class
    {
        List<T> List { get; }
        IEnumerable<T> All { get; }
        T GetByCode(int code);
        void Refresh();
    }
    public interface IEntity
    {
        int Code { get; }
    }
}
