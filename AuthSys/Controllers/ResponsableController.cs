using AuthSys.Areas.Identity.Data;
using AuthSys.Data;
using AuthSys.Models;
using AuthSys.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthSys.Controllers
{
    [Authorize]

    public class ResponsableController : Controller
    {
        private readonly AuthDbContext authDbContext;

        public ResponsableController(AuthDbContext authDbContext)
        {
            this.authDbContext = authDbContext;
        }
        [HttpGet] 
        public async Task<IActionResult> Index()
        {
           var responsables = await authDbContext.Responsable.ToListAsync();
            return View(responsables);
        }
        [HttpGet]
        public IActionResult AddResponsable()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AddResponsableModelView addResponsableRequest)

        {
            var responsable = new Responsables()
            {
                Id = Guid.NewGuid(),
                Name = addResponsableRequest.Name,
                Email = addResponsableRequest.Email,
                PhoneNumber = addResponsableRequest.PhoneNumber,
                DateOfBirdth = addResponsableRequest.DateOfBirdth,
                NameEntreprise = addResponsableRequest.NameEntreprise
            };
            await authDbContext.Responsable.AddAsync(responsable);
            await authDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task< IActionResult> View(Guid id)
        {
          var responsable=  await authDbContext.Responsable.FirstOrDefaultAsync(x => x.Id == id);
            if(responsable!= null)
            {
              var viewModel = new UpdateResponsable()

               {
                Id = responsable.Id,
                Name = responsable.Name,
                Email = responsable.Email,
                PhoneNumber = responsable.PhoneNumber,
                DateOfBirdth = responsable.DateOfBirdth,
                NameEntreprise = responsable.NameEntreprise

            };
                return await Task.Run( ()=>View("View",viewModel));
            }
           
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> View(UpdateResponsable model)
        {
            var responsable = await authDbContext.Responsable.FindAsync(model.Id);
            if(responsable != null)
            {
                responsable.Name = model.Name;
                responsable.Email = model.Email;
                responsable.PhoneNumber = model.PhoneNumber;
                responsable.DateOfBirdth = model.DateOfBirdth;
                responsable.NameEntreprise = model.NameEntreprise;

                await authDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateResponsable model)
        {
            var responsable = await  authDbContext.Responsable.FindAsync(model.Id);
            if(responsable != null)
            {
                authDbContext.Responsable.Remove(responsable);
                await authDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }



    }
}

