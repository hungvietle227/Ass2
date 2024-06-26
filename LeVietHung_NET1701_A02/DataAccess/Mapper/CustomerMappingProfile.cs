﻿using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;

namespace WPF.Utilities.Mappers
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile()
        {
            CreateMap<Customer, CustomCustomer>().ForMember(dest => dest.CustomerStatus, opt => opt.MapFrom(src => MapStatus(src.CustomerStatus))).ReverseMap();
            CreateMap<RegisterRequestDTO, Customer>()
            .ForMember(c => c.CustomerStatus, options => options.Ignore())
            .ForMember(c => c.BookingReservations, options => options.Ignore());
            CreateMap<Customer, RegisterRequestDTO>();
        }
        private string MapStatus(byte? CustomerStatus)
        {
            if (CustomerStatus == null)
            {
                return null;
            }
            else if (CustomerStatus == 0)
            {
                return "InActive";
            }
            else if (CustomerStatus == 1)
            {
                return "Active";
            }
            else
            {
                throw new InvalidOperationException("Invalid roomStatus value");
            }
        }
    }
}
