using AutoMapper;
using BloodDonation.DataAccess;
using System;
using System.Collections.Generic;
using System.Text;

namespace BloodDonation.Business.Managers
{
    abstract public class BaseManager
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;

        public BaseManager(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
    }
}
