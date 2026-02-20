using System;
using AutoMapper;
using LMSBackend.Application.Features.CRAM.Dtos;
using LMSBackend.Application.Features.CRAM.Enum;
using LMSBackend.Domain.Entities;

namespace LMSBackend.Application.Features.CRAM.Mapping
{
    public class LoanAppDetailsProfile : Profile
    {
        public LoanAppDetailsProfile()
        {
            CreateMap<LoanDetailsApp, LoanAppDetailsDto>()
                .ForMember(dest => dest.LMSLoanAppId, opt => opt.MapFrom(src => src.LMSLoanAppId))
                .ForMember(dest => dest.LoanAppCode, opt => opt.MapFrom(src => src.LoanAppCode))
                .ForMember(dest => dest.DateOfApplication, opt => opt.MapFrom(src => src.DateOfApplication))
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
                .ForMember(dest => dest.DepartureDate, opt => opt.MapFrom(src => src.DepartureDate))
                .ForMember(dest => dest.LoanTypeId, opt => opt.MapFrom(src => (LoanTypeId)src.LoanTypeId))
                .ForMember(dest => dest.Amount, opt => opt.MapFrom(src => src.Amount))
                .ForMember(dest => dest.ApprovedAmount, opt => opt.MapFrom(src => src.ApprovedAmount))
                .ForMember(dest => dest.ApprovedTerms, opt => opt.MapFrom(src => src.ApprovedTerms))
                .ForMember(dest => dest.CRARecommendation, opt => opt.MapFrom(src => src.CRARecommendation))
                .ForMember(dest => dest.Origin, opt => opt.MapFrom(src => src.Origin))
                .ForMember(dest => dest.LCMobileNumber, opt => opt.MapFrom(src => src.LCMobileNumber))
                .ForMember(dest => dest.LCSocialMedia, opt => opt.MapFrom(src => src.LCSocialMedia))
                .ForMember(dest => dest.CRARemarks, opt => opt.MapFrom(src => src.CRARemarks));
        }
    }
}