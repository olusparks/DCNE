using DCNE_Cake.Filters;
using DCNE_Cake.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace DCNE_Cake.Controllers
{
    [InitializeSimpleMembership]
    public class RoleController : Controller
    {
        private AppContext db = new AppContext();
        //
        // GET: /Role/

        public ActionResult RoleIndex()
        {
            var roles = Roles.GetAllRoles();
            return View(roles);
        }

        //GET
        /// <summary>
        /// Create a Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        public ActionResult RoleCreate()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleCreate(string roleName)
        {
            if (!Roles.RoleExists(roleName))
            {
                Roles.CreateRole(roleName);
                return RedirectToAction("RoleIndex", "Role");
            }
            return View();
        }

        /// <summary>
        /// Delete a Role
        /// </summary>
        /// <param name="roleName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string roleName)
        {
            Roles.DeleteRole(roleName);
            return RedirectToAction("RoleIndex", "Role");
        }

        /// <summary>
        /// Add Role To User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult RoleAddToUser()
        {
            //Get a list of users and roles from DB
            ViewBag.UserList = new SelectList(db.UserProfiles, "Username", "Username");
            ViewBag.RoleList = new SelectList(Roles.GetAllRoles());
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string userName, string roleName)
        {
             if (Roles.IsUserInRole(userName, roleName))
            {
                ViewBag.Message = "User exists in Role";
            }
            else
            {
                Roles.AddUserToRole(userName, roleName);
                ViewBag.Message = "User added successfully";
            }

            ViewBag.RoleList = new SelectList(Roles.GetAllRoles());
            ViewBag.UserList = new SelectList(db.UserProfiles, "Username", "Username");
            return View();
        }

        /// <summary>
        /// Get Roles
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult GetRoles(string userName)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                ViewBag.RoleForThisUser = Roles.GetRolesForUser(userName);
                ViewBag.RoleList = new SelectList(Roles.GetAllRoles());
                ViewBag.UserList = new SelectList(db.UserProfiles, "Username", "Username");
            }

            return View("RoleAddToUser");
        }

        /// <summary>
        /// Delete role for a user
        /// </summary>
        /// <param name="roleName"></param>
        /// <param name="userName"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleforUser(string roleName, string userName)
        {
            ViewBag.RoleList = new SelectList(Roles.GetAllRoles());
            ViewBag.UserList = new SelectList(db.UserProfiles, "Username", "Username");

            if (Roles.IsUserInRole(userName, roleName))
            {
                Roles.RemoveUserFromRole(userName, roleName);
                ViewBag.Message = "Role removed from user successfully";
            }
            else
            {
                ViewBag.Message = "User does not belong to selected role";
            }
            return View("RoleAddToUser");
        }
    }
}
