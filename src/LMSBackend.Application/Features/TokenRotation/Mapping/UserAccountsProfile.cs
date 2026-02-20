using System.Globalization;
using System.Text.Json;
using AutoMapper;
using LMSBackend.Application.Features.TokenRotation.Dtos;
using LMSBackend.Domain.Entities;

namespace LMSBackend.Application.Features.TokenRotation.Mapping
{
    public class UserAccountsProfile : Profile
    {
        public UserAccountsProfile()
        {
            // Map the API response properties to the UserAccounts class properties
            CreateMap<JsonElement, UserAccountsDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.GetProperty("id").GetString())))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.GetProperty("firstName").GetString()))
                .ForMember(dest => dest.MiddleName, opt => opt.MapFrom(src => src.GetProperty("middleName").GetString()))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.GetProperty("lastName").GetString()))
                .ForMember(dest => dest.Suffix, opt => opt.MapFrom(src => src.GetProperty("suffix").GetString()))
                .ForMember(dest => dest.Birthdate, opt => opt.MapFrom(src => src.GetProperty("birthdate").GetString()))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.GetProperty("username").GetString()))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.GetProperty("password").GetString()))
                .ForMember(dest => dest.Department, opt => opt.MapFrom(src => src.GetProperty("department").GetString()))
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.GetProperty("role").GetInt32()))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => src.GetProperty("branch").GetString()))
                .ForMember(dest => dest.MobileNo, opt => opt.MapFrom(src => src.GetProperty("mobileNo").GetString()))
                .ForMember(dest => dest.FbProfile, opt => opt.MapFrom(src => src.GetProperty("fbProfile").GetString()))
                .ForMember(dest => dest.AffiliateLink, opt => opt.MapFrom(src => src.GetProperty("affiliateLink").GetString()))
                .ForMember(dest => dest.Stat, opt => opt.MapFrom(src => src.GetProperty("stat").GetInt32()))
                .ForMember(dest => dest.AssignedUser, opt => opt.MapFrom(src => src.GetProperty("assignedUser").GetString() ?? null))
                .ForMember(dest => dest.IsOnline, opt => opt.MapFrom(src => src.GetProperty("isOnline").GetInt32() == 1))
                .ForMember(dest => dest.SessionTimeout, opt => opt.MapFrom(src => src.GetProperty("sessionTimeout").GetInt32()))
                .ForMember(dest => dest.UpTime, opt => opt.MapFrom(src => src.GetProperty("upTime").GetString()))
                .ForMember(dest => dest.UrlKey, opt => opt.MapFrom(src => src.GetProperty("urlKey").GetString()))
                .ForMember(dest => dest.Otp, opt => opt.MapFrom(src => src.GetProperty("otp").GetString()))
                .ForMember(dest => dest.OTPLock, opt => opt.MapFrom(src => src.GetProperty("otpLock").GetInt32()))
                .ForMember(dest => dest.OtpRequired, opt => opt.MapFrom(src => src.GetProperty("otpRequired").GetInt32()))
                .ForMember(dest => dest.SessionPingDate, opt => opt.MapFrom(src => src.GetProperty("sessionPingDate").GetString() ?? null))
                .ForMember(dest => dest.RecUser, opt => opt.MapFrom(src => src.GetProperty("recUser").GetString() ?? null))
                .ForMember(dest => dest.RecDate, opt => opt.MapFrom(src => src.GetProperty("recDate").GetString()))
                .ForMember(dest => dest.ModUser, opt => opt.MapFrom(src => src.GetProperty("modUser").GetString() ?? null))
                .ForMember(dest => dest.ModDate, opt => opt.MapFrom(src => src.GetProperty("modDate").GetString() ?? null))
                .ForMember(dest => dest.PasswordDate, opt => opt.MapFrom(src => src.GetProperty("passwordDate").GetString() ?? null))
                .ForMember(dest => dest.SessionKeys, opt => opt.MapFrom(src => src.GetProperty("sessionKeys").GetString() ?? null))
                .ForMember(dest => dest.OTPTimeStamp, opt => opt.MapFrom(src => src.GetProperty("otpTimeStamp").GetString() ?? null))
                .ForMember(dest => dest.Company, opt => opt.MapFrom(src => src.GetProperty("company").GetString() ?? null));
        }
    }
}