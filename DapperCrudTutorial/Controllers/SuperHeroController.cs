using Dapper;
using DapperCrudTutorial.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperCrudTutorial.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class SuperHeroController : ControllerBase
	{
		private readonly IConfiguration _config;

		public SuperHeroController(IConfiguration config)
		{
			_config = config;
		}

		[HttpGet]
		public async Task<ActionResult<List<SuperHero>>> GetAllSuperHeros()
		{
			using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
			IEnumerable<SuperHero> heros = await SelectAllHeroes(connection);
			return Ok(heros);
		}

		[HttpGet("{heroId}")]
		public async Task<ActionResult<SuperHero>> GetHero(int heroId)
		{
			using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));

			try
			{
				var hero = await connection.QueryFirstAsync<SuperHero>("SELECT * FROM SuperHeros WHERE Id = @Id",
					new { Id = heroId });

				return Ok(hero);
			}
			catch (Exception ex)
			{
				return NotFound();
			}
		}

		[HttpPost]
		public async Task<ActionResult<List<SuperHero>>> CreateHero(SuperHero hero)
		{
			using var connection = new SqlConnection(_config.GetConnectionString("DefaultConnection"));
			await connection.ExecuteAsync("");
			return Ok(heros);
		}
		private static async Task<IEnumerable<SuperHero>> SelectAllHeroes(SqlConnection connection)
		{
			return await connection.QueryAsync<SuperHero>("SELECT * FROM SuperHeros");
		}
	}
}
