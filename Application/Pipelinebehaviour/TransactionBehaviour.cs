using MediatR;
using MovieReservationSystemAPI.Infrastructure;

namespace MovieReservationSystemAPI.Application;

public class TransactionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse> {
    private readonly MovieReservationSystemApiDbContext _context;

    public TransactionBehaviour(MovieReservationSystemApiDbContext context) {
        _context = context;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken) {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try {
            var response = await next();

            await _context.SaveChangesAsync(cancellationToken);
            await transaction.CommitAsync(cancellationToken);

            return response;

        } catch {
            await transaction.RollbackAsync(cancellationToken);
            throw;
        }
    }
}