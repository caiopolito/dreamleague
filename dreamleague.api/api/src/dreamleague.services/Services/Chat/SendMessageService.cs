using dreamleague.domain.Aggregates.SendMessage;
using dreamleague.domain.Infrastructure;
using dreamleague.domain.Services.Chat;
using dreamleague.shared.Services;

namespace dreamleague.services.Services.Chat
{
    public class SendMessageService : GenericService<SendMessageRequest, SendMessageResponse>, ISendMessageService
    {
        private readonly IUnitOfWork unitOfWork;
        public SendMessageService
            (
                IUnitOfWork unitOfWork
            )
        {
            this.unitOfWork = unitOfWork;
        }
        protected override async Task<SendMessageResponse> OnExecute(SendMessageRequest request)
        {
            return await unitOfWork.ChatRepository.InsertAndRetrieveMessagesAsync(request);
        }
    }
}
