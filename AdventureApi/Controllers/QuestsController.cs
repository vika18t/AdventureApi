using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AdventureApi.Models;

namespace AdventureApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class QuestsController : ControllerBase
	{
		
		static List<Quest> quests = new()
		{
			new() { Id=1, Title="Пропавший амулет", Difficulty=1, Reward=100,  Location="Деревня", IsDangerous=false },
			new() { Id=2, Title="Лесные разбойники", Difficulty=2, Reward=250,  Location="Лес",     IsDangerous=true  },
			new() { Id=3, Title="Тайна старой башни", Difficulty=3, Reward=500,  Location="Башня",   IsDangerous=true  },
			new() { Id=4, Title="Сундук капитана", Difficulty=2, Reward=450,  Location="Порт",    IsDangerous=false },
			new() { Id=5, Title="Огненный голем", Difficulty=4, Reward=900,  Location="Горы",    IsDangerous=true  },
			new() { Id=6, Title="Зелье для мага", Difficulty=1, Reward=150,  Location="Город",   IsDangerous=false },
			new() { Id=7, Title="Ледяная пещера", Difficulty=5, Reward=1500, Location="Север",   IsDangerous=true  },
			new() { Id=8, Title="Забытый храм", Difficulty=3, Reward=650,  Location="Джунгли", IsDangerous=true  }
		};

		static List<Player> players = new()
		{
			new() { Id=1, Nickname="Лис", Level=1 },
			new() { Id=2, Nickname="Рыцарь_77", Level=3 },
			new() { Id=3, Nickname="Тень", Level=5 }
		};
		static List<QuestStartRequest> requests = new();

		[HttpGet]
		public IActionResult GetAll()
		{
			return Ok(quests);
		}

		[HttpGet("{id:int}")]
		public IActionResult GetById([FromRoute] int id)
		{
			var quest = quests.FirstOrDefault(q => q.Id == id);
			if (quest == null) 
			return NotFound();
			return Ok(quest);
		}

		[HttpGet("search")]
		public IActionResult Search([FromQuery] int difficulty)
		{
			var result = quests.Where(q => q.Difficulty == difficulty).ToList();
			return Ok(result);
		}

		[HttpGet("filter")]
		public IActionResult Filter(
			[FromQuery] int minReward = 0,
			[FromQuery] bool dangerous = false)
		{
			var result = quests.Where(q => q.Reward >= minReward && q.IsDangerous == dangerous).ToList();
			return Ok(result);
		}

		[HttpGet("random")]
		public IActionResult GetRandom()
		{
			int index = Random.Shared.Next(quests.Count);
			return Ok(quests[index]);
		}

		[HttpPost("start")]
		public IActionResult Start([FromBody] QuestStartRequest request)
		{
			var player = players.FirstOrDefault(p => p.Id == request.PlayerId);
			var quest = quests.FirstOrDefault(q => q.Id == request.QuestId);

			if (player == null || quest == null)
				return NotFound("Игрок или задание не найдены");

			if (player.Level < quest.Difficulty)
				return BadRequest("Ваш уровень слишком низкий");

			if (quest.IsDangerous)
			{
				bool hasShield = request.Equipment
					.Any(e => e.Equals("щит", StringComparison.OrdinalIgnoreCase));
				if (!hasShield)
					return BadRequest("Для этого задания нужен щит");
			}

			requests.Add(request);
			return Ok("Успешно");

		}

		[HttpPost("nickname")]
		public IActionResult Nickname([FromForm] string nickname)
		{
			if (string.IsNullOrWhiteSpace(nickname))
				return BadRequest("Никнейм не может быть пустым");

			return Ok( $"Приветствую, {nickname}");
		}

		[HttpGet("secret")]
		public IActionResult Secret([FromQuery] string? code)
		{
			if (code == "DRAGON2026")
			{
				return Ok(new
				{
					title = "Пещера дракона",
					difficulty = 5,
					reward = 3000
				});
			}
			return NotFound();
		}
	}
}