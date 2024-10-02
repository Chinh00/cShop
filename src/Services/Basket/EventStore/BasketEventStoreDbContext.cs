using cShop.Infrastructure.EventStore;
using Microsoft.EntityFrameworkCore;

namespace EventStore;

public class BasketEventStoreDbContext(DbContextOptions options) : EventStoreDbContextBase(options);