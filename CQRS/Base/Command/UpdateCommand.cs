using System.Reflection;
using InvoiceAPI.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace InvoiceAPI.CQRS.Base.Command
{
    public class UpdateCommand<T> : IRequest<T> where T : class
    {
        public int Id { get; set; }  // Entity ID to update
        public T? Entity { get; set; } // Updated entity data
    }
    public class UpdateCommandHandler<T> : IRequestHandler<UpdateCommand<T>, T> where T : class
    {
        private readonly InvoiceDbContext _context;
        private readonly ILogger <UpdateCommandHandler<T>> _logger;

        public UpdateCommandHandler(InvoiceDbContext context, ILogger<UpdateCommandHandler<T>> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<T> Handle(UpdateCommand<T> request, CancellationToken cancellationToken)
        {
            try
            {
                // 1️⃣ Get DbSet for entity
                var dbSet = _context.Set<T>();

                // 2️⃣ Find existing entity by Id
                var existingEntity = await dbSet.FindAsync(new object[] { request.Id }, cancellationToken);
                if (existingEntity == null)
                    throw new KeyNotFoundException($"Entity {typeof(T).Name} with Id {request.Id} not found.");

                // 3️⃣ Get list of primary key property names
                var keyProperties = _context.Model
                                            .FindEntityType(typeof(T))
                                            .FindPrimaryKey()
                                            .Properties;
                var keyNames = new List<string>();
                for (int i = 0; i < keyProperties.Count; i++)
                {
                    keyNames.Add(keyProperties[i].Name);
                }

                // 4️⃣ Loop through all properties of the entity
                var props = typeof(T).GetProperties();
                for (int i = 0; i < props.Length; i++)
                {
                    var prop = props[i];

                    // 5️⃣ Skip primary key properties
                    if (keyNames.Contains(prop.Name))
                        continue;

                    // 6️⃣ Get value from the incoming entity
                    var newValue = prop.GetValue(request.Entity);

                    // 7️⃣ Only update if value is not null
                    if (newValue != null)
                    {
                        prop.SetValue(existingEntity, newValue);
                    }
                }

                // 8️⃣ Save changes
                await _context.SaveChangesAsync(cancellationToken);

                // 9️⃣ Return updated entity
                return existingEntity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating entity of type {typeof(T).Name} with Id {request.Id}");
                throw;
            }
        }
    }
}
