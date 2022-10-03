using iTut.Constants;
using iTut.Data;
using iTut.Models;
using iTut.Models.Users;
using iTut.Models.ViewModels.HOD;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using iTut.Models.HOD.Leave;
using iTut.Models.HOD;

using Grade = iTut.Models.HOD.Grade;
using System.Collections.Generic;
using System.Net;
using System;

namespace iTut.Controllers
{
    [Authorize(Roles = RoleConstants.HOD)]
    public class HODController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<HODController> _logger;

        public HODController(ApplicationDbContext context, UserManager<ApplicationUser> userManager, ILogger<HODController> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public ActionResult EmployeIndex()
        {
            return View(_context.Employee.ToList());
        }


        public ActionResult Details(int? id)
        {
            List<EmployeeVM> empList = new List<EmployeeVM>();
            List<EmployeeEducation> EduList = new List<EmployeeEducation>();
            List<EmploymentHistory> HistoryList = new List<EmploymentHistory>();
            List<JobInfo> jobInfo = new List<JobInfo>();

            
            var employee = _context.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var empEdu = _context.EmployeeEducation.Where(i => i.EmployeeId == id).ToList();
            var empHist = _context.EmploymentHistory.Where(i => i.EmployeeId == id).ToList();
            var job = _context.JobInfo.Where(i => i.EmployeeId == id).FirstOrDefault();
            #region retrieve Education
            foreach (var data in empEdu)
            {
                var educationLavel = _context.EducationLevel.Where(i => i.Id == data.EducationLevelId).FirstOrDefault();
                var title = _context.ExamTitle.Where(i => i.EducationLevelId == educationLavel.Id).FirstOrDefault();

                EmployeeEducation ed = new EmployeeEducation();

                ed.Id = data.Id;
                ed.EducationLevelId = data.EducationLevelId;
                ed.EducationLevel = educationLavel;
                ed.ExamTitle = title;
                ed.ExamTitleId = data.ExamTitleId;
                ed.Major = data.Major;
                ed.InstituteName = data.InstituteName;
                ed.ResultType = data.ResultType;
                ed.CGPA = data.CGPA;
                ed.Scale = data.Scale;
                ed.Marks = data.Marks;
                ed.PassingYear = data.PassingYear;
                ed.Achievement = data.Achievement;
                ed.EmployeeId = data.EmployeeId;
                EduList.Add(ed);

            }
            #endregion
            #region retrieve History
            foreach (var data in empHist)
            {
                EmploymentHistory hist = new EmploymentHistory();
                hist.Id = data.Id;
                hist.CompanyName = data.CompanyName;
                hist.CompanyLocation = data.CompanyLocation;
                hist.Designation = data.Designation;
                hist.From = data.From;
                hist.To = data.To;
                hist.EmployeeId = data.EmployeeId;
                HistoryList.Add(hist);

            }
            #endregion
            #region retrieve JobInfo
            jobInfo.Add(job);
            #endregion
            #region bind in Viewmodel
            EmployeeVM vm = new EmployeeVM
            {
                Id = employee.Id,
                Name = employee.Name,
                FatherName = employee.FatherName,
                MotherName = employee.MotherName,
                Gender = employee.Gender,
                DOB = employee.DOB,
                Religion = employee.Religion,
                NID = employee.NID,
                PresentAddress = employee.PresentAddress,
                PermanentAddress = employee.PermanentAddress,
                Phone = employee.Phone,
                Email = employee.Email,
                UserName = employee.UserName,
                EmployeeEducation = EduList,
                EmploymentHistory = HistoryList,
                JobInfo = jobInfo


            };
            empList.Add(vm);
            #endregion

            ViewBag.EducationLevelId = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame");
            ViewBag.ExamTitleId = new SelectList(_context.ExamTitle, "Id", "TitleName");
            return View(empList.FirstOrDefault());
        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public ActionResult CreateEducation(EmployeeVM viewModel, int id)
        {
            try
            {
                EmployeeEducation edu = new EmployeeEducation();

                edu.EducationLevelId = viewModel.EducationLevelId;
                edu.ExamTitleId = viewModel.ExamTitleId;
                edu.Major = viewModel.Major;
                edu.InstituteName = viewModel.InstituteName;
                edu.Scale = viewModel.Scale;
                edu.CGPA = viewModel.CGPA;
                edu.Marks = viewModel.Marks;
                edu.PassingYear = viewModel.PassingYear;
                edu.Duration = viewModel.Duration;
                edu.Achievement = viewModel.Achievement;
                edu.EmployeeId = id;
                _context.EmployeeEducation.Add(edu);
                _context.SaveChanges();
                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {

                //TempData["Toastr"] = Toastr.DbError(ex.Message);
            }

            return RedirectToAction("Details", new { id = id });
        }

        [HttpPost]
        public ActionResult CreateEmployment(EmployeeVM viewModel, int id)
        {
            try
            {
                EmploymentHistory hist = new EmploymentHistory();

                hist.EmployeeId = id;
                hist.CompanyName = viewModel.CompanyName;
                hist.CompanyLocation = viewModel.CompanyLocation;
                hist.Designation = viewModel.Designation;
                hist.From = viewModel.From;
                hist.To = viewModel.To;

                _context.EmploymentHistory.Add(hist);
                _context.SaveChanges();

                return RedirectToAction("Details", new { id = id });
            }
            catch (Exception ex)
            {

                //TempData["Toastr"] = Toastr.DbError(ex.Message);
            }

            return RedirectToAction("Details", new { id = id });
        }
       



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Employe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Employe/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("EmployeIndex");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult PrintEmployee(int? id)
        {

            List<EmployeeVM> empList = new List<EmployeeVM>();
            List<EmployeeEducation> EduList = new List<EmployeeEducation>();
            List<EmploymentHistory> HistoryList = new List<EmploymentHistory>();
            List<JobInfo> jobInfo = new List<JobInfo>();

            
            var employee = _context.Employee.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            var empEdu = _context.EmployeeEducation.Where(i => i.EmployeeId == id).ToList();
            var empHist = _context.EmploymentHistory.Where(i => i.EmployeeId == id).ToList();
            var job = _context.JobInfo.Where(i => i.EmployeeId == id).FirstOrDefault();
            #region retrieve Education
            foreach (var data in empEdu)
            {
                var educationLavel = _context.EducationLevel.Where(i => i.Id == data.EducationLevelId).FirstOrDefault();
                var title = _context.ExamTitle.Where(i => i.EducationLevelId == educationLavel.Id).FirstOrDefault();

                EmployeeEducation ed = new EmployeeEducation();

                ed.Id = data.Id;
                ed.EducationLevelId = data.EducationLevelId;
                ed.EducationLevel = educationLavel;
                ed.ExamTitle = title;
                ed.ExamTitleId = data.ExamTitleId;
                ed.Major = data.Major;
                ed.InstituteName = data.InstituteName;
                ed.ResultType = data.ResultType;
                ed.CGPA = data.CGPA;
                ed.Scale = data.Scale;
                ed.Marks = data.Marks;
                ed.PassingYear = data.PassingYear;
                ed.Achievement = data.Achievement;
                ed.EmployeeId = data.EmployeeId;
                EduList.Add(ed);

            }
            #endregion
            #region retrieve History
            foreach (var data in empHist)
            {
                EmploymentHistory hist = new EmploymentHistory();
                hist.Id = data.Id;
                hist.CompanyName = data.CompanyName;
                hist.CompanyLocation = data.CompanyLocation;
                hist.Designation = data.Designation;
                hist.From = data.From;
                hist.To = data.To;
                hist.EmployeeId = data.EmployeeId;
                HistoryList.Add(hist);

            }
            #endregion
            #region retrieve JobInfo
            jobInfo.Add(job);
            #endregion
            #region bind in Viewmodel
            EmployeeVM vm = new EmployeeVM
            {
                Id = employee.Id,
                Name = employee.Name,
                FatherName = employee.FatherName,
                MotherName = employee.MotherName,
                Gender = employee.Gender,
                DOB = employee.DOB,
                
                Religion = employee.Religion,
                
                NID = employee.NID,
                PresentAddress = employee.PresentAddress,
                PermanentAddress = employee.PermanentAddress,
                Phone = employee.Phone,
                Email = employee.Email,
                
                UserName = employee.UserName,
                EmployeeEducation = EduList,
                EmploymentHistory = HistoryList,
                JobInfo = jobInfo


            };
            empList.Add(vm);
            #endregion
            return View(empList.FirstOrDefault());
        }
        // GET: HODController
        public ActionResult Index()
        {
            var hod = _context.HOD.Where(p => p.UserId == _userManager.GetUserId(User)).FirstOrDefault();

            var viewModel = new HODIndexViewModel
            {
                HodUser = hod,
            };

            return View(viewModel);
        }



        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // GET: HOD/Admissions

        public async Task<IActionResult> AdmissionsIndex()
        {
            var applicationDbContext = _context.Admission.Include(a => a.Group).Include(a => a.Session).Include(a => a.Student).Include(a => a.StudentClass);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: HOD/Admissions/Details/5
        public async Task<IActionResult> AdmissionsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admission
                .Include(a => a.Group)
                .Include(a => a.Session)
                .Include(a => a.Student)
                .Include(a => a.StudentClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }
        // GET: HOD/Admissions/Create

        public IActionResult AdmissionsCreate()
        {
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name");
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name");
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id");
            return View();
        }

        // POST: HOD/Admissions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdmissionsCreate([Bind("Id,AdmissionDate,PreviousSchool,PreviousSchoolAddrs,Extension,SessionId,StudentClassId,GroupId,StudentId")] Admission admission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(admission);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdmissionsIndex));
            }
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", admission.GroupId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", admission.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", admission.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", admission.StudentClassId);
            return View(admission);
        }

        // GET: HOD/Admissions/Edit/5
        public async Task<IActionResult> AdmissionsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admission.FindAsync(id);
            if (admission == null)
            {
                return NotFound();
            }
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", admission.GroupId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", admission.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", admission.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", admission.StudentClassId);
            return View(admission);
        }

        // POST: HOD/Admissions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdmissionsEdit(int id, [Bind("Id,AdmissionDate,PreviousSchool,PreviousSchoolAddrs,Extension,SessionId,StudentClassId,GroupId,StudentId")] Admission admission)
        {
            if (id != admission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(admission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdmissionExists(admission.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdmissionsIndex));
            }
            ViewData["GroupId"] = new SelectList(_context.Group, "Id", "Name", admission.GroupId);
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", admission.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", admission.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", admission.StudentClassId);
            return View(admission);
        }

        // GET: HOD/Admissions/Delete/5
        public async Task<IActionResult> AdmissionsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var admission = await _context.Admission
                .Include(a => a.Group)
                .Include(a => a.Session)
                .Include(a => a.Student)
                .Include(a => a.StudentClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (admission == null)
            {
                return NotFound();
            }

            return View(admission);
        }

        // POST: HOD/Admissions/Delete/5
        [HttpPost, ActionName("AdmissionsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdmissionsDeleteConfirmed(int id)
        {
            var admission = await _context.Admission.FindAsync(id);
            _context.Admission.Remove(admission);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdmissionExists(int id)
        {
            return _context.Admission.Any(e => e.Id == id);
        }

        // GET: AdminLeaveRequestViewVMs

        public async Task<IActionResult> AdminLeaveRequestViewVMsIndex()
        {

            return View(await _context.AdminLeaveRequestViewVM.ToListAsync());
        }

        // GET: AdminLeaveRequestViewVMs/Details/5
        public async Task<IActionResult> AdminLeaveRequestViewVMsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminLeaveRequestViewVM = await _context.AdminLeaveRequestViewVM
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminLeaveRequestViewVM == null)
            {
                return NotFound();
            }

            return View(adminLeaveRequestViewVM);
        }

        // GET: AdminLeaveRequestViewVMs/Create

        public IActionResult AdminLeaveRequestViewVMsCreate()
        {
            return View();
        }

        // POST: AdminLeaveRequestViewVMs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLeaveRequestViewVMsCreate([Bind("Id,TotalRequests,ApprovedRequests,PendingRequests,RejectedRequests")] AdminLeaveRequestViewVM adminLeaveRequestViewVM)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adminLeaveRequestViewVM);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AdminLeaveRequestViewVMsIndex));
            }
            return View(adminLeaveRequestViewVM);
        }

        // GET: AdminLeaveRequestViewVMs/Edit/5
        public async Task<IActionResult> AdminLeaveRequestViewVMsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminLeaveRequestViewVM = await _context.AdminLeaveRequestViewVM.FindAsync(id);
            if (adminLeaveRequestViewVM == null)
            {
                return NotFound();
            }
            return View(adminLeaveRequestViewVM);
        }

        // POST: AdminLeaveRequestViewVMs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLeaveRequestViewVMsEdit(int id, [Bind("Id,TotalRequests,ApprovedRequests,PendingRequests,RejectedRequests")] AdminLeaveRequestViewVM adminLeaveRequestViewVM)
        {
            if (id != adminLeaveRequestViewVM.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adminLeaveRequestViewVM);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminLeaveRequestViewVMExists(adminLeaveRequestViewVM.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AdminLeaveRequestViewVMsIndex));
            }
            return View(adminLeaveRequestViewVM);
        }

        // GET: AdminLeaveRequestViewVMs/Delete/5
        public async Task<IActionResult> AdminLeaveRequestViewVMsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adminLeaveRequestViewVM = await _context.AdminLeaveRequestViewVM
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adminLeaveRequestViewVM == null)
            {
                return NotFound();
            }

            return View(adminLeaveRequestViewVM);
        }

        // POST: AdminLeaveRequestViewVMs/Delete/5
        [HttpPost, ActionName("AdminLeaveRequestViewVMsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLeaveRequestViewVMsDeleteConfirmed(int id)
        {
            var adminLeaveRequestViewVM = await _context.AdminLeaveRequestViewVM.FindAsync(id);
            _context.AdminLeaveRequestViewVM.Remove(adminLeaveRequestViewVM);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdminLeaveRequestViewVMExists(int id)
        {
            return _context.AdminLeaveRequestViewVM.Any(e => e.Id == id);
        }

        // GET: AssignRolls
        public async Task<IActionResult> AssignRollsIndex()
        {
            var applicationDbContext = _context.AssignRoll.Include(a => a.Session).Include(a => a.Student).Include(a => a.StudentClass);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: AssignRolls/Details/5
        public async Task<IActionResult> AssignRollsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignRoll = await _context.AssignRoll
                .Include(a => a.Session)
                .Include(a => a.Student)
                .Include(a => a.StudentClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignRoll == null)
            {
                return NotFound();
            }

            return View(assignRoll);
        }

        // GET: AssignRolls/Create
        public IActionResult AssignRollsCreate()
        {
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name");
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id");
            return View();
        }

        // POST: AssignRolls/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRollsCreate([Bind("Id,Roll,SessionId,StudentClassId,StudentId")] AssignRoll assignRoll)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignRoll);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AssignRollsIndex));
            }
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", assignRoll.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", assignRoll.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", assignRoll.StudentClassId);
            return View(assignRoll);
        }

        // GET: AssignRolls/Edit/5
        public async Task<IActionResult> AssignRollsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignRoll = await _context.AssignRoll.FindAsync(id);
            if (assignRoll == null)
            {
                return NotFound();
            }
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", assignRoll.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", assignRoll.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", assignRoll.StudentClassId);
            return View(assignRoll);
        }

        // POST: AssignRolls/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRollsEdit(int id, [Bind("Id,Roll,SessionId,StudentClassId,StudentId")] AssignRoll assignRoll)
        {
            if (id != assignRoll.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignRoll);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignRollExists(assignRoll.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AssignRollsIndex));
            }
            ViewData["SessionId"] = new SelectList(_context.Session, "Id", "Id", assignRoll.SessionId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", assignRoll.StudentId);
            ViewData["StudentClassId"] = new SelectList(_context.StudentClass, "Id", "Id", assignRoll.StudentClassId);
            return View(assignRoll);
        }

        // GET: AssignRolls/Delete/5
        public async Task<IActionResult> AssignRollsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignRoll = await _context.AssignRoll
                .Include(a => a.Session)
                .Include(a => a.Student)
                .Include(a => a.StudentClass)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignRoll == null)
            {
                return NotFound();
            }

            return View(assignRoll);
        }

        // POST: AssignRolls/Delete/5
        [HttpPost, ActionName("AssignRollsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignRollsDeleteConfirmed(int id)
        {
            var assignRoll = await _context.AssignRoll.FindAsync(id);
            _context.AssignRoll.Remove(assignRoll);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AssignRollsIndex));
        }

        private bool AssignRollExists(int id)
        {
            return _context.AssignRoll.Any(e => e.Id == id);
        }

        // GET: AssignStuffs
        public async Task<IActionResult> AssignStuffsIndex()
        {
            return View(await _context.AssignStuff.ToListAsync());
        }

        // GET: AssignStuffs/Details/5
        public async Task<IActionResult> AssignStuffsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignStuff = await _context.AssignStuff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignStuff == null)
            {
                return NotFound();
            }

            return View(assignStuff);
        }

        // GET: AssignStuffs/Create
        public IActionResult AssignStuffsCreate()
        {
            return View();
        }

        // POST: AssignStuffs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignStuffsCreate([Bind("Id")] AssignStuff assignStuff)
        {
            if (ModelState.IsValid)
            {
                _context.Add(assignStuff);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(AssignStuffsIndex));
            }
            return View(assignStuff);
        }

        // GET: AssignStuffs/Edit/5
        public async Task<IActionResult> AssignStuffsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignStuff = await _context.AssignStuff.FindAsync(id);
            if (assignStuff == null)
            {
                return NotFound();
            }
            return View(assignStuff);
        }

        // POST: AssignStuffs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignStuffsEdit(int id, [Bind("Id")] AssignStuff assignStuff)
        {
            if (id != assignStuff.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(assignStuff);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AssignStuffExists(assignStuff.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(AssignStuffsIndex));
            }
            return View(assignStuff);
        }

        // GET: AssignStuffs/Delete/5
        public async Task<IActionResult> AssignStuffsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var assignStuff = await _context.AssignStuff
                .FirstOrDefaultAsync(m => m.Id == id);
            if (assignStuff == null)
            {
                return NotFound();
            }

            return View(assignStuff);
        }

        // POST: AssignStuffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignStuffsDeleteConfirmed(int id)
        {
            var assignStuff = await _context.AssignStuff.FindAsync(id);
            _context.AssignStuff.Remove(assignStuff);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(AssignStuffsIndex));
        }

        private bool AssignStuffExists(int id)
        {
            return _context.AssignStuff.Any(e => e.Id == id);
        }

        // GET: ClassFees
        public async Task<IActionResult> ClassFeesIndex()
        {
            var applicationDbContext = _context.ClassFee.Include(c => c.ClassName);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ClassFees/Details/5

        public async Task<IActionResult> ClassFeesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classFee = await _context.ClassFee
                .Include(c => c.ClassName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classFee == null)
            {
                return NotFound();
            }

            return View(classFee);
        }

        // GET: ClassFees/Create
        public IActionResult ClassFeesCreate()
        {
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name");
            return View();
        }

        // POST: ClassFees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("/HOD/ClassFees/Create")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClassFeesCreate([Bind("Id,AdmissionFee,ClassNameId")] ClassFee classFee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(classFee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ClassFeesIndex));
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", classFee.ClassNameId);
            return View(classFee);
        }

        // GET: ClassFees/Edit/5

        public async Task<IActionResult> ClassFeesEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classFee = await _context.ClassFee.FindAsync(id);
            if (classFee == null)
            {
                return NotFound();
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", classFee.ClassNameId);
            return View(classFee);
        }

        // POST: ClassFees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClassFeesEdit(int id, [Bind("Id,AdmissionFee,ClassNameId")] ClassFee classFee)
        {
            if (id != classFee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(classFee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassFeeExists(classFee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ClassFeesIndex));
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", classFee.ClassNameId);
            return View(classFee);
        }

        // GET: ClassFees/Delete/5
        public async Task<IActionResult> ClassFeesDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var classFee = await _context.ClassFee
                .Include(c => c.ClassName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (classFee == null)
            {
                return NotFound();
            }

            return View(classFee);
        }

        // POST: ClassFees/Delete/5

        [HttpPost, ActionName("ClassFeesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClassFeesDeleteConfirmed(int id)
        {
            var classFee = await _context.ClassFee.FindAsync(id);
            _context.ClassFee.Remove(classFee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ClassFeesIndex));
        }

        private bool ClassFeeExists(int id)
        {
            return _context.ClassFee.Any(e => e.Id == id);
        }

        // GET: ClassNames
        public async Task<IActionResult> ClassNamesIndex()
        {
            return View(await _context.ClassName.ToListAsync());
        }

        // GET: ClassNames/Details/5
        public async Task<IActionResult> ClassNamesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var className = await _context.ClassName
                .FirstOrDefaultAsync(m => m.ID == id);
            if (className == null)
            {
                return NotFound();
            }

            return View(className);
        }

        // GET: ClassNames/Create
        public IActionResult ClassNamesCreate()
        {
            return View();
        }

        // POST: ClassNames/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClassNamesCreate([Bind("ID,Name")] ClassName className)
        {
            if (ModelState.IsValid)
            {
                _context.Add(className);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ClassNamesIndex));
            }
            return View(className);
        }

        // GET: ClassNames/Edit/5
        public async Task<IActionResult> ClassNamesEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var className = await _context.ClassName.FindAsync(id);
            if (className == null)
            {
                return NotFound();
            }
            return View(className);
        }

        // POST: ClassNames/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClassNamesEdit(int id, [Bind("ID,Name")] ClassName className)
        {
            if (id != className.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(className);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClassNameExists(className.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ClassNamesIndex));
            }
            return View(className);
        }

        // GET: ClassNames/Delete/5
        public async Task<IActionResult> ClassNamesDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var className = await _context.ClassName
                .FirstOrDefaultAsync(m => m.ID == id);
            if (className == null)
            {
                return NotFound();
            }

            return View(className);
        }

        // POST: ClassNames/Delete/5
        [HttpPost, ActionName("ClassNamesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClassNamesDeleteConfirmed(int id)
        {
            var className = await _context.ClassName.FindAsync(id);
            _context.ClassName.Remove(className);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ClassNamesIndex));
        }

        private bool ClassNameExists(int id)
        {
            return _context.ClassName.Any(e => e.ID == id);
        }

        // GET: Courses
        public async Task<IActionResult> CoursesIndex()
        {
            var applicationDbContext = _context.Course.Include(c => c.ClassName);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Courses/Details/5
        public async Task<IActionResult> CoursesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.ClassName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult CoursesCreate()
        {
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name");
            return View();
        }

        // POST: Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CoursesCreate([Bind("Id,Name,Code,Theory,Mcq,Practical,Total,ClassNameId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _context.Add(course);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(CoursesIndex));
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", course.ClassNameId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public async Task<IActionResult> CoursesEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", course.ClassNameId);
            return View(course);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CoursesEdit(int id, [Bind("Id,Name,Code,Theory,Mcq,Practical,Total,ClassNameId")] Course course)
        {
            if (id != course.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(course);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseExists(course.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(CoursesIndex));
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", course.ClassNameId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public async Task<IActionResult> CoursesDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var course = await _context.Course
                .Include(c => c.ClassName)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("CoursesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CoursesDeleteConfirmed(int id)
        {
            var course = await _context.Course.FindAsync(id);
            _context.Course.Remove(course);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(CoursesIndex));
        }

        private bool CourseExists(int id)
        {
            return _context.Course.Any(e => e.Id == id);
        }

        // GET: Designations
        public async Task<IActionResult> DesignationsIndex()
        {
            return View(await _context.Designation.ToListAsync());
        }

        // GET: Designations/Details/5

        public async Task<IActionResult> DesignationsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await _context.Designation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (designation == null)
            {
                return NotFound();
            }

            return View(designation);
        }

        // GET: Designations/Create

        public IActionResult DesignationsCreate()
        {
            return View();
        }

        // POST: Designations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DesignationsCreate([Bind("Id,Name")] Designation designation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(designation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(designation);
        }

        // GET: Designations/Edit/5

        public async Task<IActionResult> DesignationsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await _context.Designation.FindAsync(id);
            if (designation == null)
            {
                return NotFound();
            }
            return View(designation);
        }

        // POST: Designations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DesignationsEdit(int id, [Bind("Id,Name")] Designation designation)
        {
            if (id != designation.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(designation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DesignationExists(designation.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(DesignationsIndex));
            }
            return View(designation);
        }

        // GET: Designations/Delete/5

        public async Task<IActionResult> DesignationsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var designation = await _context.Designation
                .FirstOrDefaultAsync(m => m.Id == id);
            if (designation == null)
            {
                return NotFound();
            }

            return View(designation);
        }

        // POST: Designations/Delete/5

        [HttpPost, ActionName("DesignationsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DesignationsDeleteConfirmed(int id)
        {
            var designation = await _context.Designation.FindAsync(id);
            _context.Designation.Remove(designation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(DesignationsIndex));
        }

        private bool DesignationExists(int id)
        {
            return _context.Designation.Any(e => e.Id == id);
        }

        // GET: EducationLevels

        public async Task<IActionResult> EducationLevelsIndex()
        {
            return View(await _context.EducationLevel.ToListAsync());
        }

        // GET: EducationLevels/Details/5
        public async Task<IActionResult> EducationLevelsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationLevel == null)
            {
                return NotFound();
            }

            return View(educationLevel);
        }

        // GET: EducationLevels/Create
        public IActionResult EducationLevelsCreate()
        {
            return View();
        }

        // POST: EducationLevels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EducationLevelsCreate([Bind("Id,EducationLevelNaame")] EducationLevel educationLevel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(educationLevel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EducationLevelsIndex));
            }
            return View(educationLevel);
        }

        // GET: EducationLevels/Edit/5

        public async Task<IActionResult> EducationLevelsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevel.FindAsync(id);
            if (educationLevel == null)
            {
                return NotFound();
            }
            return View(educationLevel);
        }

        // POST: EducationLevels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EducationLevelsEdit(int id, [Bind("Id,EducationLevelNaame")] EducationLevel educationLevel)
        {
            if (id != educationLevel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(educationLevel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EducationLevelExists(educationLevel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(EducationLevelsIndex));
            }
            return View(educationLevel);
        }

        // GET: EducationLevels/Delete/5
        public async Task<IActionResult> EducationLevelsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var educationLevel = await _context.EducationLevel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (educationLevel == null)
            {
                return NotFound();
            }

            return View(educationLevel);
        }

        // POST: EducationLevels/Delete/5
        [HttpPost, ActionName("EducationLevelsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EducationLevelsDeleteConfirmed(int id)
        {
            var educationLevel = await _context.EducationLevel.FindAsync(id);
            _context.EducationLevel.Remove(educationLevel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(EducationLevelsIndex));
        }

        private bool EducationLevelExists(int id)
        {
            return _context.EducationLevel.Any(e => e.Id == id);
        }


        // GET: HOD/Employees
        public async Task<IActionResult> EmployeesIndex()
        {
            return View(await _context.Employee.ToListAsync());
        }

        // GET: HOD/Employees/Details/5

        public async Task<IActionResult> EmployeesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: HOD/Employees/Create

        public IActionResult EmployeesCreate()
        {
            return View();
        }

        // POST: HOD/Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeesCreate([Bind("Id,Name,FatherName,MotherName,Gender,DOB,MaritalStatus,Religion,Nationality,NID,PresentAddress,PermanentAddress,Phone,Email,UserName")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(EmployeesIndex));
            }
            return View(employee);
        }

        // GET: HOD/Employees/Edit/5

        public async Task<IActionResult> EmployeesEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: HOD/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeesEdit(int id, [Bind("Id,Name,FatherName,MotherName,Gender,DOB,MaritalStatus,Religion,Nationality,NID,PresentAddress,PermanentAddress,Phone,Email,UserName")] Employee employee)
        {
            if (id != employee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(EmployeesIndex));
            }
            return View(employee);
        }

        // GET: HOD/Employees/Delete/5

        public async Task<IActionResult> EmployeesDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: HOD/Employees/Delete/5

        [HttpPost, ActionName("EmployeesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmployeesDeleteConfirmed(int id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(EmployeesIndex));
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }

        // GET: EmploymentHistories

        public async Task<IActionResult> EmploymentHistoriesIndex()
        {
            var applicationDbContext = _context.EmploymentHistory.Include(e => e.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: EmploymentHistories/Details/5

        public async Task<IActionResult> EmploymentHistoriesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentHistory = await _context.EmploymentHistory
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employmentHistory == null)
            {
                return NotFound();
            }

            return View(employmentHistory);
        }

        // GET: EmploymentHistories/Create

        public IActionResult EmploymentHistoriesCreate()
        {
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email");
            return View();
        }

        // POST: EmploymentHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmploymentHistoriesCreate([Bind("Id,CompanyName,CompanyLocation,Designation,From,To,EmployeeId")] EmploymentHistory employmentHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employmentHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", employmentHistory.EmployeeId);
            return View(employmentHistory);
        }

        // GET: EmploymentHistories/Edit/5

        public async Task<IActionResult> EmploymentHistoriesEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentHistory = await _context.EmploymentHistory.FindAsync(id);
            if (employmentHistory == null)
            {
                return NotFound();
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", employmentHistory.EmployeeId);
            return View(employmentHistory);
        }

        // POST: EmploymentHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmploymentHistoriesEdit(int id, [Bind("Id,CompanyName,CompanyLocation,Designation,From,To,EmployeeId")] EmploymentHistory employmentHistory)
        {
            if (id != employmentHistory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employmentHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploymentHistoryExists(employmentHistory.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(EmploymentHistoriesIndex));
            }
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", employmentHistory.EmployeeId);
            return View(employmentHistory);
        }

        // GET: EmploymentHistories/Delete/5

        public async Task<IActionResult> EmploymentHistoriesDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentHistory = await _context.EmploymentHistory
                .Include(e => e.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employmentHistory == null)
            {
                return NotFound();
            }

            return View(employmentHistory);
        }

        // POST: EmploymentHistories/Delete/5

        [HttpPost, ActionName("EmploymentHistoriesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EmploymentHistoriesDeleteConfirmed(int id)
        {
            var employmentHistory = await _context.EmploymentHistory.FindAsync(id);
            _context.EmploymentHistory.Remove(employmentHistory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmploymentHistoryExists(int id)
        {
            return _context.EmploymentHistory.Any(e => e.Id == id);
        }

        // GET: ExamTitles
        public async Task<IActionResult> ExamTitlesIndex()
        {
            var applicationDbContext = _context.ExamTitle.Include(e => e.EducationLevel);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ExamTitles/Details/5

        public async Task<IActionResult> ExamTitlesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examTitle = await _context.ExamTitle
                .Include(e => e.EducationLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examTitle == null)
            {
                return NotFound();
            }

            return View(examTitle);
        }

        // GET: ExamTitles/Create

        public IActionResult ExamTitlesCreate()
        {
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame");
            return View();
        }

        // POST: ExamTitles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExamTitlesCreate([Bind("Id,TitleName,EducationLevelId")] ExamTitle examTitle)
        {
            if (ModelState.IsValid)
            {
                _context.Add(examTitle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ExamTitlesIndex));
            }
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame", examTitle.EducationLevelId);
            return View(examTitle);
        }

        // GET: ExamTitles/Edit/5

        public async Task<IActionResult> ExamTitlesEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examTitle = await _context.ExamTitle.FindAsync(id);
            if (examTitle == null)
            {
                return NotFound();
            }
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame", examTitle.EducationLevelId);
            return View(examTitle);
        }

        // POST: ExamTitles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExamTitlesEdit(int id, [Bind("Id,TitleName,EducationLevelId")] ExamTitle examTitle)
        {
            if (id != examTitle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(examTitle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExamTitleExists(examTitle.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EducationLevelId"] = new SelectList(_context.EducationLevel, "Id", "EducationLevelNaame", examTitle.EducationLevelId);
            return View(examTitle);
        }

        // GET: ExamTitles/Delete/5

        public async Task<IActionResult> ExamTitlesDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var examTitle = await _context.ExamTitle
                .Include(e => e.EducationLevel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (examTitle == null)
            {
                return NotFound();
            }

            return View(examTitle);
        }

        // POST: ExamTitles/Delete/5

        [HttpPost, ActionName("ExamTitlesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExamTitlesDeleteConfirmed(int id)
        {
            var examTitle = await _context.ExamTitle.FindAsync(id);
            _context.ExamTitle.Remove(examTitle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ExamTitlesIndex));
        }

        private bool ExamTitleExists(int id)
        {
            return _context.ExamTitle.Any(e => e.Id == id);
        }

        // GET: FeeTypes
        public async Task<IActionResult> FeeTypesIndex()
        {
            return View(await _context.FeeType.ToListAsync());
        }

        // GET: FeeTypes/Details/5

        public async Task<IActionResult> FeeTypesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeType = await _context.FeeType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feeType == null)
            {
                return NotFound();
            }

            return View(feeType);
        }

        // GET: FeeTypes/Create

        public IActionResult FeeTypesCreate()
        {
            return View();
        }

        // POST: FeeTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FeeTypesCreate([Bind("Id,Name")] FeeType feeType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(feeType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(feeType);
        }

        // GET: FeeTypes/Edit/5

        public async Task<IActionResult> FeeTypesEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeType = await _context.FeeType.FindAsync(id);
            if (feeType == null)
            {
                return NotFound();
            }
            return View(feeType);
        }

        // POST: FeeTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FeeTypesEdit(int id, [Bind("Id,Name")] FeeType feeType)
        {
            if (id != feeType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(feeType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FeeTypeExists(feeType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(FeeTypesIndex));
            }
            return View(feeType);
        }

        // GET: FeeTypes/Delete/5

        public async Task<IActionResult> FeeTypesDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var feeType = await _context.FeeType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (feeType == null)
            {
                return NotFound();
            }

            return View(feeType);
        }

        // POST: FeeTypes/Delete/5

        [HttpPost, ActionName("FeeTypesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FeeTypesDeleteConfirmed(int id)
        {
            var feeType = await _context.FeeType.FindAsync(id);
            _context.FeeType.Remove(feeType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(FeeTypesIndex));
        }

        private bool FeeTypeExists(int id)
        {
            return _context.FeeType.Any(e => e.Id == id);
        }


        // GET: Grades

        public async Task<IActionResult> GradesIndex()
        {
            return View(await _context.Grade.ToListAsync());
        }

        // GET: Grades/Details/5

        public async Task<IActionResult> GradesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // GET: Grades/Create

        public IActionResult GradesCreate()
        {
            return View();
        }

        // POST: Grades/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GradesCreate([Bind("Id,Name,LowestMark,HighestMark")] Grade grade)
        {
            if (ModelState.IsValid)
            {
                _context.Add(grade);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GradesIndex));
            }
            return View(grade);
        }

        // GET: Grades/Edit/5

        public async Task<IActionResult> GradesEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade.FindAsync(id);
            if (grade == null)
            {
                return NotFound();
            }
            return View(grade);
        }

        // POST: Grades/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GradesEdit(int id, [Bind("Id,Name,LowestMark,HighestMark")] Grade grade)
        {
            if (id != grade.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(grade);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GradeExists(grade.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GradesIndex));
            }
            return View(grade);
        }

        // GET: Grades/Delete/5

        public async Task<IActionResult> GradesDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var grade = await _context.Grade
                .FirstOrDefaultAsync(m => m.Id == id);
            if (grade == null)
            {
                return NotFound();
            }

            return View(grade);
        }

        // POST: Grades/Delete/5

        [HttpPost, ActionName("GradesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GradesDeleteConfirmed(int id)
        {
            var grade = await _context.Grade.FindAsync(id);
            _context.Grade.Remove(grade);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GradesIndex));
        }

        private bool GradeExists(int id)
        {
            return _context.Grade.Any(e => e.Id == id);
        }

        // GET: Groups

        public async Task<IActionResult> GroupsIndex()
        {
            return View(await _context.Group.ToListAsync());
        }

        // GET: Groups/Details/5

        public async Task<IActionResult> GroupsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // GET: Groups/Create

        public IActionResult GroupsCreate()
        {
            return View();
        }

        // POST: Groups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GroupsCreate([Bind("Id,Name")] Group @group)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@group);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GroupsIndex));
            }
            return View(@group);
        }

        // GET: Groups/Edit/5

        public async Task<IActionResult> GroupsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group.FindAsync(id);
            if (@group == null)
            {
                return NotFound();
            }
            return View(@group);
        }

        // POST: Groups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GroupsEdit(int id, [Bind("Id,Name")] Group @group)
        {
            if (id != @group.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@group);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroupExists(@group.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GroupsIndex));
            }
            return View(@group);
        }

        // GET: Groups/Delete/5

        public async Task<IActionResult> GroupsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @group = await _context.Group
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@group == null)
            {
                return NotFound();
            }

            return View(@group);
        }

        // POST: Groups/Delete/5

        [HttpPost, ActionName("GroupsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GroupsDeleteConfirmed(int id)
        {
            var @group = await _context.Group.FindAsync(id);
            _context.Group.Remove(@group);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GroupsIndex));
        }

        private bool GroupExists(int id)
        {
            return _context.Group.Any(e => e.Id == id);
        }

        // GET: Guardians
        public async Task<IActionResult> GuardiansIndex()
        {
            var applicationDbContext = _context.Guardian.Include(g => g.GuardianType).Include(g => g.Student);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Guardians/Details/5

        public async Task<IActionResult> GuardiansDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardian = await _context.Guardian
                .Include(g => g.GuardianType)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guardian == null)
            {
                return NotFound();
            }

            return View(guardian);
        }

        // GET: Guardians/Create

        public IActionResult GuardiansCreate()
        {
            ViewData["GuardianTypeId"] = new SelectList(_context.GuardianType, "Id", "Name");
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name");
            return View();
        }

        // POST: Guardians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardiansCreate([Bind("Id,Name,Phone,Email,NID,GuardianTypeId,StudentId")] Guardian guardian)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guardian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["GuardianTypeId"] = new SelectList(_context.GuardianType, "Id", "Name", guardian.GuardianTypeId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", guardian.StudentId);
            return View(guardian);
        }

        // GET: Guardians/Edit/5

        public async Task<IActionResult> GuardiansEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardian = await _context.Guardian.FindAsync(id);
            if (guardian == null)
            {
                return NotFound();
            }
            ViewData["GuardianTypeId"] = new SelectList(_context.GuardianType, "Id", "Name", guardian.GuardianTypeId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", guardian.StudentId);
            return View(guardian);
        }

        // POST: Guardians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardiansEdit(int id, [Bind("Id,Name,Phone,Email,NID,GuardianTypeId,StudentId")] Guardian guardian)
        {
            if (id != guardian.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guardian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuardianExists(guardian.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GuardiansIndex));
            }
            ViewData["GuardianTypeId"] = new SelectList(_context.GuardianType, "Id", "Name", guardian.GuardianTypeId);
            ViewData["StudentId"] = new SelectList(_context.Student, "Id", "Name", guardian.StudentId);
            return View(guardian);
        }

        // GET: Guardians/Delete/5

        public async Task<IActionResult> GuardiansDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardian = await _context.Guardian
                .Include(g => g.GuardianType)
                .Include(g => g.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guardian == null)
            {
                return NotFound();
            }

            return View(guardian);
        }

        // POST: Guardians/Delete/5

        [HttpPost, ActionName("GuardiansDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardiansDeleteConfirmed(int id)
        {
            var guardian = await _context.Guardian.FindAsync(id);
            _context.Guardian.Remove(guardian);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GuardiansIndex));
        }

        private bool GuardianExists(int id)
        {
            return _context.Guardian.Any(e => e.Id == id);
        }


        // GET: GuardianTypes

        public async Task<IActionResult> GuardianTypesIndex()
        {
            return View(await _context.GuardianType.ToListAsync());
        }

        // GET: GuardianTypes/Details/5

        public async Task<IActionResult> GuardianTypesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardianType = await _context.GuardianType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guardianType == null)
            {
                return NotFound();
            }

            return View(guardianType);
        }

        // GET: GuardianTypes/Create

        public IActionResult GuardianTypesCreate()
        {
            return View();
        }

        // POST: GuardianTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardianTypesCreate([Bind("Id,Name")] GuardianType guardianType)
        {
            if (ModelState.IsValid)
            {
                _context.Add(guardianType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(GuardianTypesIndex));
            }
            return View(guardianType);
        }

        // GET: GuardianTypes/Edit/5

        public async Task<IActionResult> GuardianTypesEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardianType = await _context.GuardianType.FindAsync(id);
            if (guardianType == null)
            {
                return NotFound();
            }
            return View(guardianType);
        }

        // POST: GuardianTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardianTypesEdit(int id, [Bind("Id,Name")] GuardianType guardianType)
        {
            if (id != guardianType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(guardianType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GuardianTypeExists(guardianType.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(GuardianTypesIndex));
            }
            return View(guardianType);
        }

        // GET: GuardianTypes/Delete/5

        public async Task<IActionResult> GuardianTypesDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var guardianType = await _context.GuardianType
                .FirstOrDefaultAsync(m => m.Id == id);
            if (guardianType == null)
            {
                return NotFound();
            }

            return View(guardianType);
        }

        // POST: GuardianTypes/Delete/5

        [HttpPost, ActionName("GuardianTypesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> GuardianTypesDeleteConfirmed(int id)
        {
            var guardianType = await _context.GuardianType.FindAsync(id);
            _context.GuardianType.Remove(guardianType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(GuardianTypesIndex));
        }

        private bool GuardianTypeExists(int id)
        {
            return _context.GuardianType.Any(e => e.Id == id);
        }

        // GET: JobInfoes

        public async Task<IActionResult> JobInfoesIndex()
        {
            var applicationDbContext = _context.JobInfo.Include(j => j.Designation).Include(j => j.Employee);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobInfoes/Details/5

        public async Task<IActionResult> JobInfoesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobInfo = await _context.JobInfo
                .Include(j => j.Designation)
                .Include(j => j.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobInfo == null)
            {
                return NotFound();
            }

            return View(jobInfo);
        }

        // GET: JobInfoes/Create

        public IActionResult JobInfoesCreate()
        {
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name");
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email");
            return View();
        }

        // POST: JobInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JobInfoesCreate([Bind("Id,DesignationId,DOJ,Salary,TotalLeave,Appointment,AppointmentExt,EmployeeId")] JobInfo jobInfo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(jobInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(JobInfoesIndex));
            }
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name", jobInfo.DesignationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", jobInfo.EmployeeId);
            return View(jobInfo);
        }

        // GET: JobInfoes/Edit/5

        public async Task<IActionResult> JobInfoesEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobInfo = await _context.JobInfo.FindAsync(id);
            if (jobInfo == null)
            {
                return NotFound();
            }
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name", jobInfo.DesignationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", jobInfo.EmployeeId);
            return View(jobInfo);
        }

        // POST: JobInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JobInfoesEdit(int id, [Bind("Id,DesignationId,DOJ,Salary,TotalLeave,Appointment,AppointmentExt,EmployeeId")] JobInfo jobInfo)
        {
            if (id != jobInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobInfoExists(jobInfo.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(JobInfoesIndex));
            }
            ViewData["DesignationId"] = new SelectList(_context.Designation, "Id", "Name", jobInfo.DesignationId);
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "Email", jobInfo.EmployeeId);
            return View(jobInfo);
        }

        // GET: JobInfoes/Delete/5

        public async Task<IActionResult> JobInfoesDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobInfo = await _context.JobInfo
                .Include(j => j.Designation)
                .Include(j => j.Employee)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobInfo == null)
            {
                return NotFound();
            }

            return View(jobInfo);
        }

        // POST: JobInfoes/Delete/5

        [HttpPost, ActionName("JobInfoesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> JobInfoesDeleteConfirmed(int id)
        {
            var jobInfo = await _context.JobInfo.FindAsync(id);
            _context.JobInfo.Remove(jobInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(JobInfoesIndex));
        }

        private bool JobInfoExists(int id)
        {
            return _context.JobInfo.Any(e => e.Id == id);
        }


        // GET: HOD/Schools
        public async Task<IActionResult> SchoolsIndex()
        {
            return View(await _context.School.ToListAsync());
        }

        // GET: HOD/Schools/Details/5

        public async Task<IActionResult> SchoolsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.School
                .FirstOrDefaultAsync(m => m.Id == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // GET: HOD/Schools/Create

        public IActionResult SchoolsCreate()
        {
            return View();
        }

        // POST: HOD/Schools/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SchoolsCreate([Bind("Id,Name,Address,PhonePrimary,PhoneAlt,Fax,Email,Logo")] School school)
        {
            if (ModelState.IsValid)
            {
                _context.Add(school);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SchoolsIndex));
            }
            return View(school);
        }

        // GET: HOD/Schools/Edit/5

        public async Task<IActionResult> SchoolsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.School.FindAsync(id);
            if (school == null)
            {
                return NotFound();
            }
            return View(school);
        }

        // POST: HOD/Schools/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SchoolsEdit(int id, [Bind("Id,Name,Address,PhonePrimary,PhoneAlt,Fax,Email,Logo")] School school)
        {
            if (id != school.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(school);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SchoolExists(school.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SchoolsIndex));
            }
            return View(school);
        }

        // GET: HOD/Schools/Delete/5

        public async Task<IActionResult> SchoolsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var school = await _context.School
                .FirstOrDefaultAsync(m => m.Id == id);
            if (school == null)
            {
                return NotFound();
            }

            return View(school);
        }

        // POST: HOD/Schools/Delete/5

        [HttpPost, ActionName("SchoolsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SchoolsDeleteConfirmed(int id)
        {
            var school = await _context.School.FindAsync(id);
            _context.School.Remove(school);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SchoolsIndex));
        }

        private bool SchoolExists(int id)
        {
            return _context.School.Any(e => e.Id == id);
        }

        // GET: Sections

        public async Task<IActionResult> SectionsIndex()
        {
            return View(await _context.Section.ToListAsync());
        }

        // GET: Sections/Details/5

        public async Task<IActionResult> SectionsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Section
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // GET: Sections/Create

        public IActionResult SectionsCreate()
        {
            return View();
        }

        // POST: Sections/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SectionsCreate([Bind("Id,Name")] Section section)
        {
            if (ModelState.IsValid)
            {
                _context.Add(section);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SectionsIndex));
            }
            return View(section);
        }

        // GET: Sections/Edit/5

        public async Task<IActionResult> SectionsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Section.FindAsync(id);
            if (section == null)
            {
                return NotFound();
            }
            return View(section);
        }

        // POST: Sections/Edit/5

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SectionsEdit(int id, [Bind("Id,Name")] Section section)
        {
            if (id != section.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(section);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SectionExists(section.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SectionsIndex));
            }
            return View(section);
        }

        // GET: Sections/Delete/5

        public async Task<IActionResult> SectionsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var section = await _context.Section
                .FirstOrDefaultAsync(m => m.Id == id);
            if (section == null)
            {
                return NotFound();
            }

            return View(section);
        }

        // POST: Sections/Delete/5

        [HttpPost, ActionName("SectionsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SectionsDeleteConfirmed(int id)
        {
            var section = await _context.Section.FindAsync(id);
            _context.Section.Remove(section);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SectionsIndex));
        }

        private bool SectionExists(int id)
        {
            return _context.Section.Any(e => e.Id == id);
        }


        // GET: Sessions

        public async Task<IActionResult> SessionsIndex()
        {
            return View(await _context.Session.ToListAsync());
        }

        // GET: Sessions/Details/5

        public async Task<IActionResult> SessionsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Session
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // GET: Sessions/Create

        public IActionResult SessionsCreate()
        {
            return View();
        }

        // POST: Sessions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SessionsCreate([Bind("Id,Name")] Session session)
        {
            if (ModelState.IsValid)
            {
                _context.Add(session);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(SessionsIndex));
            }
            return View(session);
        }

        // GET: Sessions/Edit/5

        public async Task<IActionResult> SessionsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Session.FindAsync(id);
            if (session == null)
            {
                return NotFound();
            }
            return View(session);
        }

        // POST: Sessions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SessionsEdit(int id, [Bind("Id,Name")] Session session)
        {
            if (id != session.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(session);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SessionExists(session.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(SessionsIndex));
            }
            return View(session);
        }

        // GET: Sessions/Delete/5

        public async Task<IActionResult> SessionsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var session = await _context.Session
                .FirstOrDefaultAsync(m => m.Id == id);
            if (session == null)
            {
                return NotFound();
            }

            return View(session);
        }

        // POST: Sessions/Delete/5

        [HttpPost, ActionName("SessionsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SessionsDeleteConfirmed(int id)
        {
            var session = await _context.Session.FindAsync(id);
            _context.Session.Remove(session);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(SessionsIndex));
        }

        private bool SessionExists(int id)
        {
            return _context.Session.Any(e => e.Id == id);
        }


        // GET: Shifts

        public async Task<IActionResult> ShiftsIndex()
        {
            return View(await _context.Shift.ToListAsync());
        }

        // GET: Shifts/Details/5

        public async Task<IActionResult> ShiftsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: Shifts/Create

        public IActionResult ShiftsCreate()
        {
            return View();
        }

        // POST: Shifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShiftsCreate([Bind("Id,Name")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ShiftsIndex));
            }
            return View(shift);
        }

        // GET: Shifts/Edit/5

        public async Task<IActionResult> ShiftsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            return View(shift);
        }

        // POST: Shifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShiftsEdit(int id, [Bind("Id,Name")] Shift shift)
        {
            if (id != shift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(ShiftsIndex));
            }
            return View(shift);
        }

        // GET: Shifts/Delete/5

        public async Task<IActionResult> ShiftsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: Shifts/Delete/5

        [HttpPost, ActionName("ShiftsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ShiftsDeleteConfirmed(int id)
        {
            var shift = await _context.Shift.FindAsync(id);
            _context.Shift.Remove(shift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(ShiftsIndex));
        }

        private bool ShiftExists(int id)
        {
            return _context.Shift.Any(e => e.Id == id);
        }

        // GET: StudentClasses

        public async Task<IActionResult> StudentClassesIndex()
        {
            var applicationDbContext = _context.StudentClass.Include(s => s.ClassName).Include(s => s.Section).Include(s => s.Shift);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: StudentClasses/Details/5

        public async Task<IActionResult> StudentClassesDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentClass = await _context.StudentClass
                .Include(s => s.ClassName)
                .Include(s => s.Section)
                .Include(s => s.Shift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentClass == null)
            {
                return NotFound();
            }

            return View(studentClass);
        }

        // GET: StudentClasses/Create

        public IActionResult StudentClassesCreate()
        {
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name");
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Name");
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name");
            return View();
        }

        // POST: StudentClasses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentClassesCreate([Bind("Id,ClassNameId,SectionId,ShiftId")] StudentClass studentClass)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentClass);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(StudentClassesIndex));
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", studentClass.ClassNameId);
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Name", studentClass.SectionId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name", studentClass.ShiftId);
            return View(studentClass);
        }

        // GET: StudentClasses/Edit/5

        public async Task<IActionResult> StudentClassesEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentClass = await _context.StudentClass.FindAsync(id);
            if (studentClass == null)
            {
                return NotFound();
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", studentClass.ClassNameId);
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Name", studentClass.SectionId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name", studentClass.ShiftId);
            return View(studentClass);
        }

        // POST: StudentClasses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentClassesEdit(int id, [Bind("Id,ClassNameId,SectionId,ShiftId")] StudentClass studentClass)
        {
            if (id != studentClass.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentClass);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentClassExists(studentClass.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(StudentClassesIndex));
            }
            ViewData["ClassNameId"] = new SelectList(_context.ClassName, "ID", "Name", studentClass.ClassNameId);
            ViewData["SectionId"] = new SelectList(_context.Section, "Id", "Name", studentClass.SectionId);
            ViewData["ShiftId"] = new SelectList(_context.Shift, "Id", "Name", studentClass.ShiftId);
            return View(studentClass);
        }

        // GET: StudentClasses/Delete/5

        public async Task<IActionResult> StudentClassesDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentClass = await _context.StudentClass
                .Include(s => s.ClassName)
                .Include(s => s.Section)
                .Include(s => s.Shift)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentClass == null)
            {
                return NotFound();
            }

            return View(studentClass);
        }

        // POST: StudentClasses/Delete/5

        [HttpPost, ActionName("StudentClassesDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentClassesDeleteConfirmed(int id)
        {
            var studentClass = await _context.StudentClass.FindAsync(id);
            _context.StudentClass.Remove(studentClass);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(StudentClassesIndex));
        }

        private bool StudentClassExists(int id)
        {
            return _context.StudentClass.Any(e => e.Id == id);
        }
        
        // GET: Students
        public async Task<IActionResult> StudentsIndex()
        {
            return View(await _context.Student.ToListAsync());
        }

        // GET: Students/Details/5

        public async Task<IActionResult> StudentsDetails(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create

        public IActionResult StudentsCreate()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentsCreate([Bind("Id,Name,FatherName,MotherName,DateOfBirth,Email,PresentAddress,ParmanentAddress,Religion,Gender")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(StudentsIndex));
            }
            return View(student);
        }

        // GET: Students/Edit/5

        public async Task<IActionResult> StudentsEdit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentsEdit(int id, [Bind("Id,Name,FatherName,MotherName,DateOfBirth,Email,PresentAddress,ParmanentAddress,Religion,Gender")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(StudentsIndex));
            }
            return View(student);
        }

        // GET: Students/Delete/5

        public async Task<IActionResult> StudentsDelete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Student
                .FirstOrDefaultAsync(m => m.Id == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5

        [HttpPost, ActionName("StudentsDelete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> StudentsDeleteConfirmed(int id)
        {
            var student = await _context.Student.FindAsync(id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(StudentsIndex));
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.Id == id);
        }
    }
}

