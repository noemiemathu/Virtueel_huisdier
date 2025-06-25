using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using System.Text.Json;
using System.IO;
using Virtueel_Huisdier.Models;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Reflection.Emit;
using System.Web;
using System.Timers;

namespace Virtueel_Huisdier.Controllers
{
    public class HuisdierController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return RedirectToAction("Game");
        }

        [HttpPost]
        public ActionResult SaveImg(Game game)
        {
            string dier = game.dier;
            return RedirectToAction("Name", game);
        }

        public IActionResult Name(Game game)
        {
            return View("Name", game);
        }

        [HttpPost]
        public ActionResult SaveName(Game game)
        {
            string name = game.name;
            string dier = game.dier;

            var updatedFile = JsonSerializer.Serialize(game);
            System.IO.File.WriteAllText("./game.json", updatedFile);

            return RedirectToAction("Game", game);
        }
        public IActionResult Game(Game game)
        {
            if (System.IO.File.Exists("./game.json"))
            {
                var file = System.IO.File.ReadAllText("./game.json");
                game = JsonSerializer.Deserialize<Game>(file);
            }

            if (!System.IO.File.Exists("./game.json") && game != null)
            {
                var file = JsonSerializer.Serialize(game);
                System.IO.File.WriteAllText("./game.json", file);
                return View("Index");
            }

            game.status.energie -= 1;
            game.status.eten -= 3;
            game.status.geluk -= 1;
            game.gif = TempData["Gif"]?.ToString() ?? "";

            if (game.status.energie <= -10 || game.status.eten <= -10 || game.status.geluk <= -10)
            {
                System.IO.File.Delete("./game.json");
                return View("GameOver");
            }

            var updatedFile = JsonSerializer.Serialize(game);
            System.IO.File.WriteAllText("./game.json", updatedFile);

            return View("Game", game);
        }
        public IActionResult DeleteGame()
        {
            System.IO.File.Delete("./game.json");
            return View("Index");
        }

        [HttpPost]
        public IActionResult Feed(Game game)
        {
            TempData["Gif"] = game.gif;

            var file = System.IO.File.ReadAllText("./game.json");
            if (string.IsNullOrWhiteSpace(file))
                return Redirect(nameof(Index));

            if (file == null)
                return Redirect(nameof(Index));


            game = JsonSerializer.Deserialize<Game>(file);
            if (game == null)
                return Redirect(nameof(Index));

            game.status.eten += 10;
            game.status.gezondheid -= 5;
            game.status.geluk += 2;
            game.status.energie -= 3;

            if (game.status.energie > 100) { game.status.energie = 100; }
            if (game.status.eten > 100) { game.status.eten = 100; }
            if (game.status.geluk > 100) { game.status.geluk = 100; }
            if (game.status.gezondheid > 100) { game.status.gezondheid = 100; }

            var updatedFile = JsonSerializer.Serialize(game);
            System.IO.File.WriteAllText("./game.json", updatedFile);

            return RedirectToAction("Game");
        }

        [HttpPost]
        public IActionResult Play(Game game)
        {
            TempData["Gif"] = game.gif;
            var file = System.IO.File.ReadAllText("./game.json");
            var model = JsonSerializer.Deserialize<Game>(file);

            model.status.eten -= 5;
            model.status.gezondheid += 3;
            model.status.geluk += 10;
            model.status.energie -= 6;

            if (model.status.energie > 100) { model.status.energie = 100; }
            if (model.status.eten > 100) { model.status.eten = 100; }
            if (model.status.geluk > 100) { model.status.geluk = 100; }
            if (model.status.gezondheid > 100) { model.status.gezondheid = 100; }

            var updatedFile = JsonSerializer.Serialize(model);
            System.IO.File.WriteAllText("./game.json", updatedFile);

            return RedirectToAction("Game");
        }

        [HttpPost]
        public IActionResult Sleep(Game game)
        {
            TempData["Gif"] = game.gif;
            var file = System.IO.File.ReadAllText("./game.json");
            var model = JsonSerializer.Deserialize<Game>(file);

            model.status.energie += 10;
            model.status.eten -= 2;
            model.status.geluk -= 2;
            model.status.gezondheid += 5;

            if (model.status.energie > 100) { model.status.energie = 100; }
            if (model.status.eten > 100) { model.status.eten = 100; }
            if (model.status.geluk > 100) { model.status.geluk = 100; }
            if (model.status.gezondheid > 100) { model.status.gezondheid = 100; }

            var updatedFile = JsonSerializer.Serialize(model);
            System.IO.File.WriteAllText("./game.json", updatedFile);

            return RedirectToAction("Game");
        }

        [HttpPost]
        public IActionResult Medicine(Game game)
        {
            TempData["Gif"] = game.gif;
            var file = System.IO.File.ReadAllText("./game.json");
            var model = JsonSerializer.Deserialize<Game>(file);

            model.status.energie += 1;
            model.status.eten += 1;
            model.status.geluk += 1;
            model.status.gezondheid += 10;

            if (model.status.energie > 100) { model.status.energie = 100; }
            if (model.status.eten > 100) { model.status.eten = 100; }
            if (model.status.geluk > 100) { model.status.geluk = 100; }
            if (model.status.gezondheid > 100) { model.status.gezondheid = 100; }

            var updatedFile = JsonSerializer.Serialize(model);
            System.IO.File.WriteAllText("./game.json", updatedFile);

            return RedirectToAction("Game");
        }

        [HttpPost]
        public ActionResult HandleForm(string yesNo)
        {
            if (yesNo == "Ja")
            {
                return RedirectToAction("Game");
            }
            else if (yesNo == "Nee")
            {
                ViewBag.Result = "Klik dan op het kruisje!!!";
            }
            return View("GameOver");
        }
    }
}
