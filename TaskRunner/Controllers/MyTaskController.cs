using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskRunner.Dto.ViewModel;
using TaskRunner.Service;

namespace TaskRunner.Controllers
{
    public class MyTaskController : Controller
    {
        private readonly ITaskService _taskService;

        public MyTaskController(ITaskService taskService)
        {
            this._taskService = taskService;
        }


        public async Task<IActionResult> Index()
        {
            List<MyTaskViewModel> taskViewModels = await _taskService.GetAllTask();

            return View(taskViewModels);
        }


        public async Task<IActionResult> Details(int? Id)
        {
            MyTaskViewModel taskViewModel = await _taskService.GetMyTaskById(Id.GetValueOrDefault());

            return View(taskViewModel);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(MyTaskViewModel task)
        {
            if (!ModelState.IsValid) throw new Exception("Invalid Model State");

            MyTaskViewModel taskView = await _taskService.SaveMyTask(task);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            MyTaskViewModel taskViewModel = await _taskService.GetMyTaskById(id.GetValueOrDefault());
            return View(taskViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(MyTaskViewModel task)
        {
            if (!ModelState.IsValid) throw new Exception("Invalid Model State");

            MyTaskViewModel taskView = await _taskService.UpdateMyTask(task);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int? id)
        {
            MyTaskViewModel taskViewModel = await _taskService.GetMyTaskById(id.GetValueOrDefault());
            return View(taskViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(MyTaskViewModel task)
        {
            if (task.Id == 0) throw new Exception("Invalid task.");

            await _taskService.DeleteMyTask(task.Id);
            return RedirectToAction("Index");
        }
    }
}
