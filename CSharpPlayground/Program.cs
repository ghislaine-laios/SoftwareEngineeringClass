using CSharpPlayground.model;

await using var context = new DatabaseContextFactory().CreateDbContext(Array.Empty<string>());
await DatabaseContextFactory.Seed(context, true);