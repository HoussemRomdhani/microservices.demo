using System;
using System.Collections.Generic;
using System.Linq;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace identity.Controllers
{
    public class ApiScopesController : Controller
    {
        private readonly ConfigurationDbContext _configurationDbContext;
        public ApiScopesController(ConfigurationDbContext configurationDbContext)
        {
            _configurationDbContext = configurationDbContext ?? throw new ArgumentNullException(nameof(configurationDbContext));
        }

        // GET: Apis
        public ActionResult Index()
        {
            var apis = _configurationDbContext.ApiScopes.ToList();
            return View(apis);
        }

        // GET: Apis/Details/5
        public ActionResult Infos(int id)
        {
            var apiDb = _configurationDbContext.ApiResources.
            Include(a => a.UserClaims).
            Include(a => a.Secrets).
            FirstOrDefault(a => a.Id == id);
            var api = new ApiResource();
            if (apiDb != null)
                api = new ApiResource
                {
                    Id = apiDb.Id,
                    Name = apiDb.Name,
                    DisplayName = apiDb.DisplayName,
                    Description = apiDb.Description
                    // UserClaims = string.Join(",", apiDb.UserClaims.Select(x => x.Type))
                };

            return View(api);
        }

        // GET: Apis/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Apis/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ApiResource api)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var userClaims = new List<string>();

                    var apiToSave =
                        new IdentityServer4.Models.ApiResource(api.Name, api.DisplayName, userClaims)
                        {
                            Description = api.Description
                            // ApiSecrets = { new IdentityServer4.Models.Secret(api.Secrets.Sha256()) }
                        };

                    _configurationDbContext.ApiResources.Add(apiToSave.ToEntity());
                    _configurationDbContext.SaveChanges();

                    TempData["message"] = $"{api.Name} has been created.";
                }
                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                return View();
            }
        }

        // GET: Apis/Edit/5
        public ActionResult Update(int id)
        {
            var apiDb = _configurationDbContext.ApiResources.FirstOrDefault(a => a.Id == id);

            if (apiDb != null)
            {
                ApiResource api = new ApiResource
                {
                    Id = apiDb.Id,
                    Name = apiDb.Name,
                    DisplayName = apiDb.DisplayName,
                    Description = apiDb.Description,
                };

                return View(api);
            }
            else
                return RedirectToAction(nameof(Index));
        }

        // POST: Apis/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(ApiResource api)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var apiDb = _configurationDbContext.ApiResources.
                                Include(a => a.UserClaims).
                                Include(a => a.Secrets).
                                 FirstOrDefault(a => a.Id == api.Id);
                    apiDb.Name = api.Name;
                    apiDb.DisplayName = api.DisplayName;
                    apiDb.Description = api.Description;
                    _configurationDbContext.ApiResources.Update(apiDb);
                    _configurationDbContext.SaveChanges();
                }

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

  //      // GET: Apis/Delete/5
  //      public ActionResult Delete(int id)
  //      {
  //          var apiDb = _configurationDbContext.ApiResources.
  //                                       Include(a => a.UserClaims).
  //Include(a => a.Secrets).
  //FirstOrDefault(a => a.Id == id);
  //          var api = new ApiResource();
  //          if (apiDb != null)
  //              api = new ApiResource
  //              {
  //                  Id = apiDb.Id,
  //                  Name = apiDb.Name,
  //                  DisplayName = apiDb.DisplayName,
  //                  //   ClaimTypes = string.Join(",", apiDb.UserClaims.Select(x => x.Type))
  //              };

  //          return View(api);
  //      }

        public ActionResult Delete(int id)
        {
            try
            {
                var apiDb = _configurationDbContext.ApiResources
                       .FirstOrDefault(c => c.Id == id);
                _configurationDbContext.ApiResources.Remove(apiDb);
                _configurationDbContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
