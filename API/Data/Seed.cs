using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.DTOs;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public class Seed
{
    public static async Task SeedUsers(AppDbContext context, CancellationToken ct = default)
    {
        if (await context.AppUsers.AnyAsync(ct))
            return;

        var memberData = await File.ReadAllTextAsync("Data/UserSeedData.json", ct);
        var members = JsonSerializer.Deserialize<List<SeedUserDto>>(memberData);

        if (members is null)
        {
            Console.WriteLine("No members in seed data");
            return;
        }

        foreach (var member in members)
        {
            using var hmac = new HMACSHA512();
            var user = new AppUser()
            {
                DisplayName = member.DisplayName ?? string.Empty,
                Email = member.Email ?? string.Empty,
                Id = member.Id,
                ImageUrl = member.ImageUrl,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("Pa$$w0rd")),
                PasswordSalt = hmac.Key,
                Member = new Member()
                {
                    Gender = member.Gender,
                    City = member.City,
                    Country = member.Country,
                    Created = member.Created,
                    DateOfBirth = member.DateOfBirth,
                    Description = member.Description,
                    DisplayName = member.DisplayName,
                    Id = member.Id,
                    ImageUrl = member.ImageUrl,
                    LastActive = member.LastActive
                }
            };

            user.Member.Photos.Add(new Photo()
            {
                Url = member.ImageUrl ?? string.Empty,
                MemberId = member.Id
            });

            context.AppUsers.Add(user);
            await context.SaveChangesAsync(ct);
        }
    }
}
