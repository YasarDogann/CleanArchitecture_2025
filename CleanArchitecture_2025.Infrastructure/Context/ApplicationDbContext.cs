using System.Security.Claims;
using CleanArchitecture_2025.Domain.Abstractions;
using CleanArchitecture_2025.Domain.Employees;
using CleanArhictecture_2025.Domain.Abstractions;
//using CleanArhictecture_2025.Domain.Employees;
//using CleanArhictecture_2025.Domain.Users;
using GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CleanArhictecture_2025.Infrastructure.Context;
internal sealed class ApplicationDbContext : DbContext ,IUnitOfWork
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Employee> Employees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}