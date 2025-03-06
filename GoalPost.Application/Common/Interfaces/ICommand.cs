using MediatR;

namespace GoalPost.Application.Common.Interfaces
{
    public interface ICommand<TResponse> : IRequest<TResponse>
    {
    }
} 