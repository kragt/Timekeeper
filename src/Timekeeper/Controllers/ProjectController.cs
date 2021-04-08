using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timekeeper.Services;
using Timekeeper.Models;
using Microsoft.Extensions.Logging;

namespace Timekeeper.Controllers
{
    public class ProjectController : Controller
    {
        private readonly ICosmosDbService _db;
        private readonly ILogger<ProjectController> _logger;

        private static string PrettyProject(Project p)
        {
            var sb = new System.Text.StringBuilder(250);
            sb.AppendFormat(" ProjectId: {0}\n", p.ProjectId);
            sb.AppendFormat("        Id: {0}\n", p.Id);
            sb.AppendFormat("      Type: {0}\n", p.Type);
            sb.AppendFormat("      Name: {0}\n", p.Name);
            sb.AppendFormat(" IsRunning: {0}\n", p.IsRunning);
            sb.AppendFormat("IsArchived: {0}\n", p.IsArchived);
            sb.AppendFormat(" StartTime: {0}", p.StartTime);

            return sb.ToString();
        }
         
        public ProjectController(ICosmosDbService cosmosDbService, ILogger<ProjectController> logger)
        {
            _db = cosmosDbService;
            _logger = logger;
        }
        // GET: ProjectController
        public async Task<ActionResult> Index()
        {
            return View(await _db.GetAllProjectsAsync());
        }

        // GET: ProjectController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProjectController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProjectController/Create
        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync([Bind("Name")] Project project)
        {
            _logger.LogInformation("Adding project...");

            project.ProjectId = Guid.NewGuid().ToString();
            project.Id = project.ProjectId;
            project.Type = "project";
            project.IsArchived = false;
            project.IsRunning = false;
            project.StartTime = DateTime.MinValue;

            string s = PrettyProject(project);
            _logger.LogInformation(s);

            try
            {
                await _db.AddProjectAsync(project);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return View();
            }
        }

        // GET: ProjectController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ProjectController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ProjectController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ProjectController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
