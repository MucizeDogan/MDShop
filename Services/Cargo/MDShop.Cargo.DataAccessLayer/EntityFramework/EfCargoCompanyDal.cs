
using MDShop.Cargo.DataAccessLayer.Abstract;
using MDShop.Cargo.DataAccessLayer.Concrete;
using MDShop.Cargo.DataAccessLayer.Repositories;
using MDShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MDShop.Cargo.DataAccessLayer.EntityFramework {
    public class EfCargoCompanyDal : GenericRepository<CargoCompany>, ICargoCompanyDal {
        public EfCargoCompanyDal(CargoContext context) : base(context) //Base kelimesi Bir sınıftan miras aldığımızda  üst sınıftaki metotları veya alanları çağırmak için kullanılır
        {
            
        }
    }
}
