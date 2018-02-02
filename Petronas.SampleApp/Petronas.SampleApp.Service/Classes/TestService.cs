using EasyAdsense.Model.Classes;
using EasyAdsense.Repository.Classes;
using EasyAdsense.Repository.Interfaces;
using EasyAdsense.Service.Interfaces;

namespace EasyAdsense.Service.Classes
{
    public class TestService<T> : ITestService<T> where T : BaseEntity
    {
        readonly UnitOfWork _unitOfWork;
        //readonly IGenericRepository<T> _repository;

        protected TestService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            //_repository = repository;
        }

        public int Test()
        {
            return 0;
        }
    }
}