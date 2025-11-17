using Project.Domain.Entities;

namespace Project.Application.Interfaces
{
    public interface IMessageService
    {
        Task<Message> SendMessageAsync(long senderId, long receiverId, string content);

        Task<IEnumerable<Message>> GetConversationAsync(long user1Id, long user2Id);

        Task MarkAsReadAsync(long messageId);
    }
}
