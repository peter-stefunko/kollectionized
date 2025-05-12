using Microsoft.EntityFrameworkCore;
using Kollectionized.Api.Models;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Kollectionized.Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users => Set<User>();
    public DbSet<PokemonCollection> PokemonCollections => Set<PokemonCollection>();
    public DbSet<PokemonDeck> PokemonDecks => Set<PokemonDeck>();
    public DbSet<PokemonCard> PokemonCards => Set<PokemonCard>();
    public DbSet<CardInstance> PokemonCardInstances => Set<CardInstance>();
    public DbSet<PokemonSet> PokemonSets => Set<PokemonSet>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PokemonDeck>()
            .HasMany(d => d.CardInstances)
            .WithMany()
            .UsingEntity<Dictionary<string, object>>(
                "pokemon_decks_cards",
                j => j.HasOne<CardInstance>().WithMany().HasForeignKey("instance_id"),
                j => j.HasOne<PokemonDeck>().WithMany().HasForeignKey("deck_id")
            );
        
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(ToSnakeCase(entity.GetTableName()!));
            
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(ToSnakeCase(property.GetColumnName(StoreObjectIdentifier.Table(entity.GetTableName()!))!));
            }
            
            foreach (var key in entity.GetKeys())
            {
                key.SetName(ToSnakeCase(key.GetName()!));
            }
            
            foreach (var fk in entity.GetForeignKeys())
            {
                fk.SetConstraintName(ToSnakeCase(fk.GetConstraintName()!));
            }
            
            foreach (var index in entity.GetIndexes())
            {
                index.SetDatabaseName(ToSnakeCase(index.GetDatabaseName()!));
            }
        }

        base.OnModelCreating(modelBuilder);
    }

    private static string ToSnakeCase(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return input;

        var result = new System.Text.StringBuilder();
        result.Append(char.ToLowerInvariant(input[0]));

        for (int i = 1; i < input.Length; ++i)
        {
            var c = input[i];
            if (char.IsUpper(c))
            {
                result.Append('_');
                result.Append(char.ToLowerInvariant(c));
            }
            else
            {
                result.Append(c);
            }
        }

        return result.ToString();
    }

}