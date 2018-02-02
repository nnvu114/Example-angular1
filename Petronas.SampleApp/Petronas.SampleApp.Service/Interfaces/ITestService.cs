using EasyAdsense.Model.Classes;

namespace EasyAdsense.Service.Interfaces
{
    public interface ITestService<T> where T : BaseEntity
    {
        int Test();
    }
}