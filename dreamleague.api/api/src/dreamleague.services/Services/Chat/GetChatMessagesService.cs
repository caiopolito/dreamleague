using dreamleague.domain.Aggregates.GetOrCreateChatInfo;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Chat;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Chat
{
    public class GetOrCreateChatInfoService : GenericService<GetOrCreateChatInfoRequest, GetOrCreateChatInfoResponse>, IGetOrCreateChatInfoService
    {
        private readonly IUnitOfWork unitOfWork;
        public GetOrCreateChatInfoService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected override async Task<GetOrCreateChatInfoResponse> OnExecute(GetOrCreateChatInfoRequest request)
        {
            var chatInfo = await unitOfWork.ChatRepository.GetChatInfoAsync(request);

            if (chatInfo is null)
                return await unitOfWork.ChatRepository.CreateChatAsync(request);

            return chatInfo;
        }
    }
}
