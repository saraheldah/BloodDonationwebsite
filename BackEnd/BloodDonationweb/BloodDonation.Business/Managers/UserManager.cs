using AutoMapper;
using BloodDonation.DataAccess;

namespace BloodDonation.Business.Managers
{
    public class UserManager : BaseManager, IUserManager
    {
        public UserManager(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
        {
        }
        public void LogIn()
        {
            var users = _unitOfWork.UserRepository.All();
        }
    }
}
