using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using System.Text;
using System.Net.Http.Headers;
using SQLiteNetExtensions.Extensions;
using SQLite;


namespace NutritionApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly ILogger<AccountsController> _logger;
        readonly SQLiteConnection conn;
        public HttpContext HttpContext { get; }
        public AccountsController(ILogger<AccountsController> logger)
        {

            conn = new SQLiteConnection(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "diary.db3"));
            conn.CreateTable<Settingss>();
            conn.CreateTable<BacklogReceived>();
            conn.CreateTable<Fixed>();
            if (conn.Table<Account>() != null)
            {
                ;
            }
            if (conn.Table<Settingss>() != null)
            {
                ;
            }

            _logger = logger;
        }

        [HttpGet]
        public IActionResult GetAccounts()
        {
            try
            {
                var accessToken = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentials = accessToken.Parameter;
                Encoding encoding = Encoding.GetEncoding("iso-8859-1");
                string usernamePassword = encoding.GetString(Convert.FromBase64String(credentials));
                int seperatorIndex = usernamePassword.IndexOf(':');
                var username = usernamePassword.Substring(0, seperatorIndex);
                var password = usernamePassword.Substring(seperatorIndex + 1);
                var data = conn.Query<Account>("SELECT * FROM Account");
                var d1 = data.Where(x => x.Username == username && x.Password == password);
                if (d1.ElementAt(0) != null)
                {
                    return Ok(d1);
                }
                else
                {
                    return Unauthorized();
                }

            }
            catch (Exception ex)
            {
                return Unauthorized();

            }

        }

        [HttpPost]
        public IActionResult AddAccount([FromBody] Account accountToAdd)
        {
            var data = conn.Query<Account>("SELECT * FROM Account");

            var d1 = data.Where(x => x.Username == accountToAdd.Username && x.Password == accountToAdd.Password);
            var d2 = data.Where(x => x.Username == accountToAdd.Username);
            if (d1.Any())
            {
                return Conflict();
            }
            if (d2.Any())
            {
                return Conflict("Username already exists");
            }
            conn.Insert(accountToAdd);

            return new OkResult();

        }


        [HttpPost]
        public IActionResult AddSettings([FromBody] Settingss settingsToAdd)
        {

            var data = conn.Query<Settingss>("select * from Settings");
            var data1 = conn.Query<Account>("select * from Account");
            var d1 = data.Where(x => x.Username == settingsToAdd.Username);
            var d2 = data1.Where(x => x.Username == settingsToAdd.Username);

            if (d1.Any())
            {
                return Conflict();
            }
            if (d2.Any())
            {
                conn.Insert(settingsToAdd);
            }
            else
            {
                return Conflict();
            }
            return new OkResult();



        }
        //patch route

        [HttpGet]
        public IActionResult GetSettings1()
        {
            var data = conn.Query<Settingss>("SELECT * from Settings");
            return Ok(data);
        }

        [HttpGet]
        public IActionResult GetSettings([FromQuery] string Username)
        {
            var data = conn.Query<Settingss>("SELECT Calories FROM Settings WHERE Username = ?", Username);
            return Ok(data);
        }


        [HttpPut]
        public IActionResult ModifySettings([FromBody] Settingss settingsToModify)
        {

            var data = conn.Query<Settingss>("DELETE FROM Settings WHERE Username = ?", settingsToModify.Username);
            conn.Insert(settingsToModify);
            return Ok(data);
        }
        [HttpPost]
        public IActionResult AddBacklog([FromBody] BacklogReceived backlogToAdd)
        {
            if (backlogToAdd.Username != "string" && backlogToAdd.Username != "")
            {
                
                conn.InsertWithChildren(backlogToAdd);
                
                if (backlogToAdd.Details.Count != 0)
                {
                    return Ok();
                }
            }


            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetBacklog([FromQuery] string Username)
        {
            var list = conn.GetAllWithChildren<BacklogReceived>(d => d.Username == Username);
            return Ok(list);
        }

        [HttpGet]
        public IActionResult GetBacklog1()
        {
            var data = conn.Query<Fixed>("SELECT * FROM Fixed" );
            return Ok(data);
        }



    }
}


