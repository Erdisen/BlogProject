using BlogProject.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Core.Service
{
    public interface ICoreService<T> where T : CoreEntity
    {
        bool Add(T item);
        bool Add(List<T> items);
        bool Remove(T item);
        bool Remove(Guid id);
        bool RemoveAll(Expression<Func<T,bool>> exp); // Belli bir LINQ ifadesine göre filtreleyip silmek için yazılan servis metodu. Metodun içine LINQ ifadesi verilecektir.
        bool Update(T item);
        T GetByID(Guid id);
        T GetByDefault(Expression<Func<T, bool>> exp); // FirsOrDefault'a benzer bir metot oluşturur.
        List<T> GetActive();
        List<T> GetDefault(Expression<Func<T, bool>> exp);
        List<T> GetAll();
        bool Activate(Guid id); // Aktifleştirmek için kullanılacak metot.
        bool Any(Expression<Func<T, bool>> exp); // LINQ ifadesi ile var mı diye sorgulama yapacağımız metot.
        int Save();// DB' de manipülasyon işleminden sonra 1 veya daha fazla satır etkilediğinde bize kaç satırın etkilendiğini döndürecek metodumuz.

        void DetachEntity(T item);


    }
}
