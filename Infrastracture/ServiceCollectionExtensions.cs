using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddInfrastructure(
		   this IServiceCollection services,
		   IConfiguration configuration,
           Assembly? applicationAssembly = null)
		{
			services
				.AddDatabase(configuration);
			return services;
		}
	}
}
