using Database.Code;

namespace Database.Code.IoC;

public class DatabaseModule : Module
{
	protected override void Load(ContainerBuilder builder)
	{
		// string contextualConnectionString = DbFactory.GetContextualConnectionString(PI.Database.IoC.IoC.Mode);
		//
		// var serviceCollection = new ServiceCollection();
		//
		// serviceCollection
		// 	.AddDbContext<FireflyDBContext>(options =>
		// 	{
		// 		options.UseSqlServer(contextualConnectionString);
		// 	});
		//
		// serviceCollection.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
		// 	.AddRoles<IdentityRole>()
		// 	.AddEntityFrameworkStores<FireflyIdentityDbContext>();
		//
		// builder.Populate(serviceCollection);
		//
		// builder.Register(context =>
		// 	{
		// 		var dbContextOptions = new DbContextOptionsBuilder<FireflyDBContext>()
		// 			.UseSqlServer(contextualConnectionString);
		//
		// 		return new FireflyDBContext(dbContextOptions.Options);
		// 	})
		// 	.As<FireflyDBContext>();
		//
		// builder.Register(context =>
		// 	{
		// 		var dbContextOptions = new DbContextOptionsBuilder<FireflyIdentityDbContext>()
		// 			.UseSqlServer(contextualConnectionString);
		//
		// 		return new FireflyIdentityDbContext(dbContextOptions.Options);
		// 	})
		// 	.As<FireflyIdentityDbContext>();
	}
}