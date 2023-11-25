using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProNotes.AppData.EFCore.Context;
using ProNotes.AppData.Entities;
using ProNotes.AppLib.MVC.Attributes;
using ProNotes.ViewModels;

namespace ProNotes.Controllers
{
    // https://elmah.io/tools/multiline-string-converter/

    [CustomAuthorize(AllowedRoles = "User, Administrator")]
    public class NotesController : Controller
    {
        MapperConfiguration? mapperConfiguration = new MapperConfiguration(cfg => cfg.CreateMap<NoteViewModel, Note>()
            .ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.SubcategoryId))
            //.ForMember(dest => dest.birimKapaliMi, o => o.Ignore())
            //.ForMember(dest => dest._insertedTime, act => act.MapFrom<DateTime>(src => DateTime.Now))
            //.ForMember(dest => dest.birimTuru_Kod_AsBigInt, act => act.MapFrom<Int32>(src => Convert.ToInt32(src.birimTuru_Kod)))
            .ReverseMap()
        );



        private readonly AppDbContext _appDbContext;

        public NotesController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }


        [MenuItem(_SubmenuDisplayName: "Note List")]
        [HttpGet]
        public IActionResult Index()
        {
            List<NoteViewModel>? notes = _appDbContext.Notes
                .Select(n=> new NoteViewModel() { Id=n.Id, CategoryId=n.CategoryId, Caption=n.Caption, Abstract=n.Abstract })
                .ToList();

            return View(model: notes);
        }

        [HttpGet]
        public IActionResult ViewNote(int id)
        {
            NoteViewModel note = _appDbContext.Notes.Where(n => n.Id == id)
                .Select(n => new NoteViewModel() { Id = n.Id, CategoryId = n.CategoryId, Caption = n.Caption, Abstract = n.Abstract, Content=n.Content, Tags=n.Tags })
                .First();

            return View(model: note);
        }

        [MenuItem(_SubmenuDisplayName: "New Note")]
        [HttpGet]
        public IActionResult Note(int? id)
        {
            if(id == null) // New Note
            {
                NoteViewModel modelData = new NoteViewModel()
                {
                    Categories = _appDbContext.Categories
                        .Where(c => c.ParentCategoryId == null)
                        .Select(i => new SelectListItem() { Text = i.CategoryText, Value = i.CategoryId.ToString() })
                        .ToList()
                    // Subcategories = will be loaded client side (AJAX) for new notes
                };

                return View(model: modelData);
            }
            else // Existing Note to be updated
            {
                NoteViewModel modelData = _appDbContext.Notes
                    .Where(n => n.Id == id)
                    .Select(n => new NoteViewModel() { 
                        Id = n.Id, CategoryId = n.CategoryId, Caption = n.Caption, Abstract = n.Abstract, Content = n.Content, Tags = n.Tags 
                    })
                    .First();

                int? ParentCategoryId = _appDbContext.Categories.Where(c => c.CategoryId == modelData.CategoryId).Select(c => c.ParentCategoryId).FirstOrDefault();

                if (ParentCategoryId != null)
                {
                    modelData.SubcategoryId = modelData.CategoryId;
                    modelData.CategoryId = ParentCategoryId.Value;
                    modelData.Subcategories = _appDbContext.Categories.Where(c => c.ParentCategoryId == ParentCategoryId.Value)
                        .Select(i => new SelectListItem() { Text = i.CategoryText, Value = i.CategoryId.ToString() })
                        .ToList();
                }

                modelData.Categories = _appDbContext.Categories.Where(c => c.ParentCategoryId == null)
                        .Select(i => new SelectListItem() { Text = i.CategoryText, Value = i.CategoryId.ToString() })
                        .ToList();

                int err = ModelState.ErrorCount;

                return View(model: modelData);
            }
        }

        [HttpPost]
        public IActionResult Note([ModelBinder(Name = "tinyEditor")] string editorContent, NoteViewModel noteViewModel)
        {
            int errCount = ModelState.ErrorCount;


            noteViewModel.Content = editorContent;           

            try
            {
                mapperConfiguration?.AssertConfigurationIsValid();
                var executionPlan = mapperConfiguration?.BuildExecutionPlan(typeof(NoteViewModel), typeof(Note));
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }

            IMapper? mapper = mapperConfiguration?.CreateMapper();

            // Note? noteEntity = mapper?.Map<NoteViewModel, Note>(noteViewModel);
            Note? noteEntity = mapper?.Map<Note>(noteViewModel);

            if (noteEntity!.Id != 0)
            {
                _appDbContext.Notes.Attach(noteEntity);
                _appDbContext.Entry(noteEntity).State = EntityState.Modified;
                noteViewModel.Id = noteEntity.Id;
                _appDbContext.SaveChanges();
            }
            else
            {
                _appDbContext.Notes.Add(noteEntity);
                _appDbContext.SaveChanges();
                noteViewModel.Id = noteEntity.Id;
            }
            

            noteViewModel.Categories = _appDbContext.Categories
                .Where(c => c.ParentCategoryId == null)
                .Select(i => new SelectListItem() { Text = i.CategoryText, Value = i.CategoryId.ToString() })
                .ToList();

            if (noteViewModel.SubcategoryId != 0)
            {
                noteViewModel.Subcategories = _appDbContext.Categories
                    .Where(c => c.ParentCategoryId == noteViewModel.CategoryId)
                    .Select(i => new SelectListItem() { Text = i.CategoryText, Value = i.CategoryId.ToString() })
                    .ToList();
            }

            

            return View(model: noteViewModel);
        }

        [HttpGet]
        public JsonResult Subcategories(int catid)
        {
            var altturler = _appDbContext.Categories
                .Where(i => i.ParentCategoryId == catid)
                .Select(i => new SelectListItem() { Text = i.CategoryText, Value = i.CategoryId.ToString() }).ToList();

            return Json(altturler);
        }






        /// <summary>
        /// TinyMCE Default image upload button handler.
        /// This handler is called by the function "UploadImage" registered for the "images_upload_handler".
        /// Uploads the given image to the uploads path with a new random name (Guid).
        /// </summary>
        /// <returns>Returns only the source (src) part of the img tag as URI string</returns>
        [HttpPost]
        [Route("/tinymce/image")]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> Upload([FromServices] IWebHostEnvironment env, IFormFile img)
        {
            try
            {
                string uploadFolder = "uploads";

                FileInfo imageFileInfo = new FileInfo(Path.Combine(env.WebRootPath, uploadFolder, img.FileName));
                FileInfo imageNewFileInfo = new FileInfo(Path.Combine(imageFileInfo.DirectoryName, $"{Guid.NewGuid().ToString()}{imageFileInfo.Extension}"));

                using (var fileStream = new FileStream(imageNewFileInfo.FullName, FileMode.Create))
                {
                    await img.CopyToAsync(fileStream);
                }

                return Json(new { location = $"{Url.Content("~/")}{uploadFolder}/{imageNewFileInfo.Name}" });
            }
            catch (Exception ex)
            {
                return Json(new { location = $"{ex.Message}" });
            }
        }

        // https://docs.sixlabors.com/
        //public System.Drawing.Image Base64ToImage()
        //{
        //    byte[] imageBytes = Convert.FromBase64String(base64String);
        //    MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);
        //    ms.Write(imageBytes, 0, imageBytes.Length);
        //    System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
        //    return image;
        //}

        public string SaveToImage(IWebHostEnvironment env, string Base64ImageStr)
        {
            string uploadFolder = "uploads";
            FileInfo imageFileInfo = new FileInfo(Path.Combine(env.WebRootPath, uploadFolder));
            FileInfo imageNewFileInfo = new FileInfo(Path.Combine(imageFileInfo.DirectoryName, $"{Guid.NewGuid().ToString()}{imageFileInfo.Extension}"));
            System.IO.File.WriteAllBytes(imageNewFileInfo.FullName, Convert.FromBase64String(Base64ImageStr));
            return "";
        }
    }
}

/*
 * 
 * 
 * 
 * Update Row if it Exists Else Insert Logic with Entity Framework [closed]
 
If you are working with attached object (object loaded from the same instance of the context) you can simply use:

if (context.ObjectStateManager.GetObjectStateEntry(myEntity).State == EntityState.Detached)
{
    context.MyEntities.AddObject(myEntity);
}

// Attached object tracks modifications automatically

context.SaveChanges();


If you can use any knowledge about the object's key you can use something like this:

if (myEntity.Id != 0)
{
    context.MyEntities.Attach(myEntity);
    context.ObjectStateManager.ChangeObjectState(myEntity, EntityState.Modified);
}
else
{
    context.MyEntities.AddObject(myEntity);
}

context.SaveChanges();
If you can't decide existance of the object by its Id you must execute lookup query:

var id = myEntity.Id;
if (context.MyEntities.Any(e => e.Id == id))
{
    context.MyEntities.Attach(myEntity);
    context.ObjectStateManager.ChangeObjectState(myEntity, EntityState.Modified);
}
else
{
    context.MyEntities.AddObject(myEntity);
}

context.SaveChanges();





 */