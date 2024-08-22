using BNA.EF1.Domain.Common.Interfaces;
using BNA.EF1.Infrastructure.Database;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace BNA.EF1.Infrastructure.EventualConsistency
{
    public class EventualConsistencyMiddleware(RequestDelegate _next)
    {
        public const string DomainEventsKey = "DomainEventsKey";

        public async Task InvokeAsync(HttpContext context, IPublisher publisher, ApplicationDbContext dbContext)
        {
            var transaction = await dbContext.Database.BeginTransactionAsync();
            context.Response.OnCompleted(async () =>
            {
                try
                {
                    if (context.Items.TryGetValue(DomainEventsKey, out var value) && value is Queue<IDomainEvent> domainEvents)
                    {
                        while (domainEvents.TryDequeue(out var nextEvent))
                        {
                            await publisher.Publish(nextEvent);
                        }
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    //Tu lógica de contingencia
                }
                finally
                {
                    await transaction.DisposeAsync();
                }
            });

            await _next(context);
        }
    }
}
