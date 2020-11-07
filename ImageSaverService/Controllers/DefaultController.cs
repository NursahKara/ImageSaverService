using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace ImageSaverService.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public IHttpActionResult RegisterUser(string username)
        {
            try
            {
                using (var ctx = new dbContext())
                {
                    var user = ctx.Users.SingleOrDefault(w => w.Username == username);
                    if (user != null)
                    {
                        return Json(new
                        {
                            success = true,
                            guid = user.Guid
                        });
                    }
                    UserModel model = new UserModel()
                    {
                        Username = username,
                        DateCreated = DateTime.UtcNow,
                        Guid = Guid.NewGuid().ToString()
                };
                    ctx.Users.Add(model);
                    ctx.SaveChanges();
                    return Json(new
                    {
                        success = true,
                        guid = model.Guid
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        [HttpPost]
        public IHttpActionResult SendImage([FromBody] SendImageModel model)
        {
            try
            {
                using (var ctx = new dbContext())
                {
                    var user = ctx.Users.SingleOrDefault(w => w.Guid == model.userGuid);
                    if (user == null)
                    {
                        return Json(new
                        {
                            success = false,
                            message = "user does not exists"
                        });
                    }
                    ImageModel imageModel = new ImageModel()
                    {
                        Guid = Guid.NewGuid().ToString(),
                        UserGuid = user.Guid
                    };
                    //var root = HttpContext.Current.Server.MapPath("/Uploads/");
                    var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Uploads/";
                    var dir = root + user.Username + "/";
                    if (!Directory.Exists(dir))
                        Directory.CreateDirectory(dir);
                    var bytes = Convert.FromBase64String(model.base64);
                    ByteToImage(bytes).Save(dir + imageModel.Guid + ".jpg", ImageFormat.Jpeg);
                    imageModel.Path = dir + imageModel.Guid + ".jpg";
                    ctx.Images.Add(imageModel);
                    ctx.SaveChanges();
                    return Json(new
                    {
                        success = true,
                        guid = imageModel.Guid,
                        path = imageModel.Path
                    });
                }
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                });
            }
        }
        [NonAction]
        public static Image ByteToImage(byte[] bytes)
        {
            var ms = new MemoryStream(bytes);
            return Image.FromStream(ms);
        }
    }
}
