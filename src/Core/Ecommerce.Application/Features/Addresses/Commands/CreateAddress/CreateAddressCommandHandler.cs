using AutoMapper;
using Ecommerce.Application.Contracts.Identity;
using Ecommerce.Application.Features.Addresses.Vms;
using Ecommerce.Application.Persistence;
using Ecommerce.Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Application.Features.Addresses.Commands.CreateAddress
{
    public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, AddressVm>
    {
        private readonly IAuthService _authService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateAddressCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IAuthService authService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authService = authService;
        }
        public async Task<AddressVm> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
        {
            var addressRecord = await _unitOfWork.Repository<Address>().GetEntityAsync(
                x => x.Username == _authService.GetSessionUser(),
                null,
                false
                );

            if (addressRecord is null)
            {
                addressRecord = new Address
                {
                    StreetAddress = request.StreetAddress,
                    City = request.City,
                    Department = request.Department,
                    PostalCode = request.PostalCode,
                    Country = request.Country,
                    Username = _authService.GetSessionUser()
                };

                _unitOfWork.Repository<Address>().AddEntity(addressRecord);

            }
            else
            {
                addressRecord.StreetAddress = request.StreetAddress;
                addressRecord.City = request.City;
                addressRecord.Department = request.Department;
                addressRecord.PostalCode = request.PostalCode;
                addressRecord.Country = request.Country;
            }

            await _unitOfWork.Complete();

            return _mapper.Map<AddressVm>(addressRecord);
        }
    }
}
