using AutoMapper;
using EnglishAI.Application.Models.AI;
using OpenAI_API.Chat;
using EnglishAI.Utils.Automapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnglishAI.Infrastructure
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            // Ignore destination properties except Messages
            CreateMap<Session, ChatRequest>(MemberList.Source)
                .ForMember(dest => dest.Messages, opt => opt.MapFrom(src => src.Items))
                .ReverseMap();

            CreateMap<SessionItem, ChatMessage>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Role))
                .ReverseMap();
        }
    }
}
