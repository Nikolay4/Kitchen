// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace Kitchen.Controllers
{
    public partial class RecipeController
    {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RecipeController() { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RecipeController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(Task<ActionResult> taskResult)
        {
            return RedirectToAction(taskResult.Result);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(ActionResult result)
        {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoutePermanent(callInfo.RouteValueDictionary);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToActionPermanent(Task<ActionResult> taskResult)
        {
            return RedirectToActionPermanent(taskResult.Result);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult setPhoto()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.setPhoto);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public virtual System.Web.Mvc.ActionResult GetRecipe()
        {
            return new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetRecipe);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public RecipeController Actions { get { return MVC.Recipe; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Recipe";
        [GeneratedCode("T4MVC", "2.0")]
        public const string NameConst = "Recipe";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass
        {
            public readonly string EditRecipe = "EditRecipe";
            public readonly string setPhoto = "setPhoto";
            public readonly string GetRecipe = "GetRecipe";
            public readonly string RecipeBook = "RecipeBook";
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNameConstants
        {
            public const string EditRecipe = "EditRecipe";
            public const string setPhoto = "setPhoto";
            public const string GetRecipe = "GetRecipe";
            public const string RecipeBook = "RecipeBook";
        }


        static readonly ActionParamsClass_EditRecipe s_params_EditRecipe = new ActionParamsClass_EditRecipe();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_EditRecipe EditRecipeParams { get { return s_params_EditRecipe; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_EditRecipe
        {
            public readonly string Id = "Id";
            public readonly string model = "model";
        }
        static readonly ActionParamsClass_setPhoto s_params_setPhoto = new ActionParamsClass_setPhoto();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_setPhoto setPhotoParams { get { return s_params_setPhoto; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_setPhoto
        {
            public readonly string Photo = "Photo";
            public readonly string id = "id";
        }
        static readonly ActionParamsClass_GetRecipe s_params_GetRecipe = new ActionParamsClass_GetRecipe();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionParamsClass_GetRecipe GetRecipeParams { get { return s_params_GetRecipe; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionParamsClass_GetRecipe
        {
            public readonly string id = "id";
        }
        static readonly ViewsClass s_views = new ViewsClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewsClass Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewsClass
        {
            static readonly _ViewNamesClass s_ViewNames = new _ViewNamesClass();
            public _ViewNamesClass ViewNames { get { return s_ViewNames; } }
            public class _ViewNamesClass
            {
                public readonly string EditRecipe = "EditRecipe";
                public readonly string GetRecipe = "GetRecipe";
                public readonly string RecipeBook = "RecipeBook";
            }
            public readonly string EditRecipe = "~/Views/Recipe/EditRecipe.cshtml";
            public readonly string GetRecipe = "~/Views/Recipe/GetRecipe.cshtml";
            public readonly string RecipeBook = "~/Views/Recipe/RecipeBook.cshtml";
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public partial class T4MVC_RecipeController : Kitchen.Controllers.RecipeController
    {
        public T4MVC_RecipeController() : base(Dummy.Instance) { }

        [NonAction]
        partial void EditRecipeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int Id);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditRecipe(int Id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditRecipe);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Id", Id);
            EditRecipeOverride(callInfo, Id);
            return callInfo;
        }

        [NonAction]
        partial void EditRecipeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, Kitchen.Models.RecipeModel model);

        [NonAction]
        public override System.Web.Mvc.ActionResult EditRecipe(Kitchen.Models.RecipeModel model)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.EditRecipe);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "model", model);
            EditRecipeOverride(callInfo, model);
            return callInfo;
        }

        [NonAction]
        partial void setPhotoOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, System.Web.HttpPostedFileBase Photo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult setPhoto(System.Web.HttpPostedFileBase Photo, int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.setPhoto);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "Photo", Photo);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            setPhotoOverride(callInfo, Photo, id);
            return callInfo;
        }

        [NonAction]
        partial void GetRecipeOverride(T4MVC_System_Web_Mvc_ActionResult callInfo, int id);

        [NonAction]
        public override System.Web.Mvc.ActionResult GetRecipe(int id)
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.GetRecipe);
            ModelUnbinderHelpers.AddRouteValues(callInfo.RouteValueDictionary, "id", id);
            GetRecipeOverride(callInfo, id);
            return callInfo;
        }

        [NonAction]
        partial void RecipeBookOverride(T4MVC_System_Web_Mvc_ActionResult callInfo);

        [NonAction]
        public override System.Web.Mvc.ActionResult RecipeBook()
        {
            var callInfo = new T4MVC_System_Web_Mvc_ActionResult(Area, Name, ActionNames.RecipeBook);
            RecipeBookOverride(callInfo);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591