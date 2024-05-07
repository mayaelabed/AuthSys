using AuthSys.Data;
using AuthSys.Models.Domain;
using AuthSys.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace AuthSys.Controllers
{
    [Authorize]

    public class ClientController : Controller
    {
        private readonly AuthDbContext authDbContext;

        public ClientController(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var client = await authDbContext.Client.ToListAsync();
            return View(client);
        }
        [HttpGet]
        public IActionResult AddClient()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddClientModelView addClientRequest)

        {
            var client = new Client()
            {
                Id = Guid.NewGuid(),
                Name = addClientRequest.Name,
                Email = addClientRequest.Email,
                PhoneNumber = addClientRequest.PhoneNumber,
                DateOfBirdth = addClientRequest.DateOfBirdth,
                Statut = addClientRequest.Statut
            };
            await authDbContext.Client.AddAsync(client);
            await authDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> View(Guid id)
        {
            var client = await authDbContext.Client.FirstOrDefaultAsync(x => x.Id == id);
            if (client != null)
            {
                var viewModel = new UpdateClient()

                {
                    Id = client.Id,
                    Name = client.Name,
                    Email = client.Email,
                    PhoneNumber = client.PhoneNumber,
                    DateOfBirdth = client.DateOfBirdth,
                    Statut = client.Statut

                };
                return await Task.Run(() => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateClient model)
        {
            var client = await authDbContext.Client.FindAsync(model.Id);
            if (client != null)
            {
                client.Name = model.Name;
                client.Email = model.Email;
                client.PhoneNumber = model.PhoneNumber;
                client.DateOfBirdth = model.DateOfBirdth;
                client.Statut = model.Statut;

                await authDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateClient model)
        {
            var client = await authDbContext.Client.FindAsync(model.Id);
            if (client != null)
            {
                authDbContext.Client.Remove(client);
                await authDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}

